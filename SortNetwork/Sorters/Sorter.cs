using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using MathUtils.Rand;
using SortNetwork.KeySets;
using SortNetwork.Switchables;

namespace SortNetwork.Sorters
{
    public interface ISorter
    {
        Guid Guid { get; }
        int KeyCount { get; }
        ISwitchable Sort(ISwitchable switchable);
        IEnumerable<ISwitch> Switches { get; }
        ISwitch SwitchAtIndex(int index);
        int SwitchCount { get; }
    }

    public static class Sorter
    {
        public static ISorter ToSorter(this IEnumerable<IKeyPair> keyPairs, Guid guid)
        {
            var keyPairList = keyPairs as IList<IKeyPair> ?? keyPairs.ToList();
            return keyPairList.Select((keyPair, index) => Switch.Make(index, keyPair)).ToSorter(guid);
        }

        public static ISorter ToSorter(this IEnumerable<ISwitch> keyPairs, Guid guid)
        {
            return new SorterImpl( guid, keyPairs );
        }

        public static IEnumerable<ISorter> ToSorters
        (
            this IEnumerable<IKeyPair> keyPairs,
            int switchesPerSorter,
            int sorterCount,
            IEnumerable<Guid> enumerableGuids
        )
        {
            var guidEnumerator = enumerableGuids.GetEnumerator();
            foreach (var chunk in keyPairs.Chunk(switchesPerSorter, sorterCount))
            {
                guidEnumerator.MoveNext();
                yield return
                    chunk.ToSorter(guidEnumerator.Current);
            }
        }

        public static int ToHashCode(this ISorter sorter)
        {
            //var lps = sorter.SwitchResultsToJson.Select(T => new Tuple<int, int>(T.Index, T.KeyPair.Index));
            return sorter.Switches.ToList()
                .Aggregate(1, (current, t) => current + (t.KeyPair.Index + t.Index + 1));
        }

        public static string StringValue(this ISorter sorter)
        {
            return sorter.Switches.Aggregate
                (
                    string.Empty,
                    (s, bv) => s + (string.IsNullOrEmpty(s) ? string.Empty : ",  ") + bv.KeyPair.ToLabel()
                );
        }

        public static TS Mutate<TS> 
        (
            this ISorter sorter,
            IGenerator<bool> mutateOrNot,
            IRandomInt rndSwitchSelector,
            IRandomGuid rndIdMaker,
            Func<Guid, IEnumerable<ISwitch>, TS> sorterMaker
        )
            where TS : ISorter
        {
            return sorterMaker
                (
                    rndIdMaker.Next(),
                    sorter.Switches.Select
                    (
                        T => Switch.Make
                        (
                            T.Index,
                            mutateOrNot.Next() ? T.KeyPair.Mutate(rndSwitchSelector) :
                                                 T.KeyPair
                        )
                    )
                );
        }

        public static IEnumerable<TS> MutateSeveral<TS>
            (
                this ISorter sorter,
                IGenerator<bool> rndMutateOrNot,
                IRandomInt rndSwitchSelector,
                IRandomGuid rndIdMaker,
                Func<Guid, IEnumerable<ISwitch>, TS> sorterMaker,
                int copyNumber
            )
            where TS : ISorter
        {
            for (var i = 0; i < copyNumber; i++)
            {
                yield return sorter.Mutate
                (
                    rndMutateOrNot,
                    rndSwitchSelector,
                    rndIdMaker,
                    sorterMaker
                );
            }
        }
    }

    class SorterImpl : ISorter
    {
        public SorterImpl(Guid guid, IEnumerable<ISwitch> switches)
        {
            _guid = guid;
            _switches = switches.ToList();

            if (_switches.Count == 0) { return; }

#if SAFE_MODE

            SafeModeCheck();
#endif
            _keyCount = _switches[0].KeyPair.KeyCount;
        }

// ReSharper disable UnusedMember.Local
        void SafeModeCheck()
// ReSharper restore UnusedMember.Local
        {
            if (_switches.AreOutOfOrder(sw => sw.Index))
            {
                throw new ArgumentException("SwitchResultsToJson are not sequentially indexed");
            }

            var keyCountGroups = Switches.GroupBy(T => T.KeyPair.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        public ISwitchable Sort(ISwitchable switchable)
        {
            var curSwitchable = switchable;

            foreach (var @switch in Switches)
            {
                if (curSwitchable.IsSorted)
                {
                    return curSwitchable;
                }
                curSwitchable = curSwitchable.Switch(@switch.KeyPair);
            }

            return curSwitchable;
        }


        private readonly List<ISwitch> _switches;
        public IEnumerable<ISwitch> Switches
        {
            get
            {
                for (var i = 0; i < _switches.Count; i++)
                {
                    yield return _switches[i];
                }
            }
        }

        public ISwitch SwitchAtIndex(int index)
        {
            return _switches[index];
        }

        public int SwitchCount
        {
            get { return _switches.Count; }
        }
    }
}
