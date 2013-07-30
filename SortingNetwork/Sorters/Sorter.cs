using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using MathUtils.Repos;
using SortingNetwork.Common;
using SortingNetwork.KeyPair;
using SortingNetwork.Switchables;
using SortingNetwork.Switches;

namespace SortingNetwork.Sorters
{
    public static class Sorter
    {
        public static ISorter<ISwitch> ToSorter(this IEnumerable<IKeyPair> keyPairs, Guid guid)
        {
            var keyPairList = keyPairs as IList<IKeyPair> ?? keyPairs.ToList();
            return keyPairList.Select((keyPair, index) => Switch.Make(index, keyPair)).ToSorter(guid);
        }

        public static ISorter<ISwitch> ToSorter(this IEnumerable<ISwitch> keyPairs, Guid guid)
        {
            return new SorterImpl( guid, keyPairs );
        }

        public static IEnumerable<ISorter<ISwitch>> ToSorters
            (
                this IKeyPairRepo keyPairRepo, 
                int switchesPerSorter,
                int sorterCount
            )
        {
            for (var i = 0; i < sorterCount; i++)
            {
                yield return 
                    keyPairRepo.GetRange(switchesPerSorter * i, switchesPerSorter).Items
                        .ToSorter(Guid.NewGuid());
            }
        }

        public static TS Mutate<TS> 
        (
            this ISorter sorter,
            IGenerator<bool> mutateOrNot,
            IRandomInt rndSwitchSelector,
            Func<Guid, IEnumerable<ISwitch>, TS> sorterMaker
        )
            where TS : ISorter
        {
            return sorterMaker
                (
                    sorter.Guid,
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
                    sorterMaker
                );
            }
        }
    }

    class SorterImpl : SorterBase<ISwitch>
    {
         public SorterImpl(Guid guid, IEnumerable<ISwitch> switches)
            : base(guid, switches)
        {
        }

        public override ISwitchable Sort(ISwitchable switchable)
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

        public override SorterType SorterType
        {
            get { return SorterType.Simple; }
        }
    }
}
