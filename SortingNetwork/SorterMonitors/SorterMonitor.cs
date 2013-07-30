using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathUtils.Rand;
using MathUtils.Repos;
using SortingNetwork.Common;
using SortingNetwork.KeyPair;
using SortingNetwork.Sorters;
using SortingNetwork.Switchables;
using SortingNetwork.Switches;

namespace SortingNetwork.SorterMonitors
{
    public interface ISorterMonitor : ISorter<ISwitchMonitor>
    {
        int SwitchesUsed { get; }
    }

    public static class SorterMonitor
    {
        public static ISorterMonitor ToSorterMonitor(this IEnumerable<ISwitch> switches, Guid guid)
        {
            return new SorterMonitorImpl(guid, switches.Select(t=>t.ToSwitchMonitor()));
        }

        public static ISorterMonitor ToSorterMonitor(this IEnumerable<IKeyPair> keyPairs, Guid guid)
        {
            var keyPairList = keyPairs as IList<IKeyPair> ?? keyPairs.ToList();
            return new SorterMonitorImpl
                (
                    Guid.NewGuid(),
                    keyPairList.Select((keyPair, index) => SwitchMonitor.Make(index, keyPair, 0))
                );
        }

        public static IEnumerable<ISorterMonitor> ToSorterMonitors
        (
            this IKeyPairRepo keyPairRepo,
            int switchesPerSorter,
            int sorterCount
        )
        {
            for (var i = 0; i < sorterCount; i++)
            {
                yield return keyPairRepo.GetRange(switchesPerSorter * i, switchesPerSorter).Items
                    .ToSorterMonitor(Guid.NewGuid());
            }
        }

        public static IEnumerable<ISorterMonitor> MutateToSorterMonitors
        (
            this ISorter sorter,
            IGenerator<bool> rndMutateOrNot,
            IRandomInt rndSwitchSelector,
            int copyNumber
        )
        {
            for (var i = 0; i < copyNumber; i++)
            {
                yield return sorter.Mutate
                    (
                        rndMutateOrNot, 
                        rndSwitchSelector,
                        (g, enumer) => enumer.Select(T => T.KeyPair).ToSorterMonitor(g)
                    );
            }
        }

        public static string LongSwitchReport(this ISorterMonitor sorterMonitor)
        {
            var sb = new StringBuilder();
            var maxSwitch = sorterMonitor.Switches.Where(T => T.UseCount > 0).Max(q => q.Index);
            foreach (var switchUsage in sorterMonitor.Switches)
            {
                if (switchUsage.Index > maxSwitch)
                {
                    return sb.ToString();
                }

                sb.Append(switchUsage.UseCount > 0
                                ? string.Format("\t[{0} : {1}]", switchUsage.KeyPair.ToLabel(), switchUsage.UseCount)
                                : string.Format("\t[{0}]", switchUsage.KeyPair.ToLabel()));
            }
            return sb.ToString();
          }
    }

    class SorterMonitorImpl : SorterBase<ISwitchMonitor>, ISorterMonitor
    {
        public SorterMonitorImpl(Guid guid, IEnumerable<ISwitchMonitor> switches)
            : base(guid, switches)
        {
        }

        public override ISwitchable Sort(ISwitchable switchable)
        {
            var lastSwitchable = switchable;

            foreach (var @switch in Switches)
            {
                if (lastSwitchable.IsSorted)
                {
                    return lastSwitchable;
                }
                var curSwitchable = lastSwitchable.Switch(@switch.KeyPair);
                if (Equals(curSwitchable, lastSwitchable))
                {
                    continue;
                }

                lastSwitchable = curSwitchable;
                @switch.UseCount++;
            }
            return lastSwitchable;
        }

        public override SorterType SorterType
        {
            get { return SorterType.Monitor; }
        }

        private int? _switchesUsed;
        public int SwitchesUsed
        {
            get
            {
                if (! _switchesUsed.HasValue)
                {
                    _switchesUsed = Switches.Count(T => T.UseCount > 0);
                }
                return _switchesUsed.Value;
            }
        }
    }
}
