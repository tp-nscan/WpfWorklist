using System;
using System.Collections.Generic;
using System.Linq;
using SortNetwork.Sorters;

namespace SortNetwork.Diff
{
    public interface ISorterPoolDiff
    {
        IEnumerable<ISorterDiff> SorterDiffs { get; }
        bool AnySwitchLevelDiffs { get; }
    }

    public static class SorterPoolDiff
    {
        public static ISorterPoolDiff Make(IEnumerable<ISorter> sorterGroupA, IEnumerable<ISorter> sorterGroupB)
        {
            return new SorterPoolDiffImpl(sorterGroupA, sorterGroupB);
        }
    }

    public class SorterPoolDiffImpl : ISorterPoolDiff
    {
        public SorterPoolDiffImpl(IEnumerable<ISorter> sorterGroupA, IEnumerable<ISorter> sorterGroupB)
        {
            var sorterDictA = sorterGroupA.ToDictionary(s => s.Guid);
            var sorterDictB = sorterGroupB.ToDictionary(s => s.Guid);

            foreach (var sorter in sorterDictA.Values)
            {
                _sorterPoolDiff.Add
                    (
                        key: sorter.Guid,
                        value: 
                            SorterDiff.Make
                            (
                                sorter, 
                                sorterDictB.ContainsKey(sorter.Guid) ? sorterDictB[sorter.Guid] : null
                            )
                    );

                if (sorterDictB.ContainsKey(sorter.Guid))
                {
                    sorterDictB.Remove(sorter.Guid);
                }
            }

            foreach (var sorter in sorterDictB.Values)
            {
                _sorterPoolDiff.Add
                (
                    key: sorter.Guid,
                    value: SorterDiff.Make(sorter, null)
                );
            }
        }

        private readonly Dictionary<Guid, ISorterDiff> _sorterPoolDiff = new Dictionary<Guid, ISorterDiff>();

        public IEnumerable<ISorterDiff> SorterDiffs
        {
            get { return _sorterPoolDiff.Values; }
        }

        public bool AnySwitchLevelDiffs
        {
            get
            {
                return SorterDiffs.Any(T => T.SwitchesAreDifferent);
            }
        }
    }
}
