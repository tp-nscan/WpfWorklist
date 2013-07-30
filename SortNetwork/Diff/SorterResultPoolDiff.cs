using System;
using System.Collections.Generic;
using System.Linq;
using SortNetwork.Results;

namespace SortNetwork.Diff
{
    public interface ISorterResultPoolDiff
    {
        IEnumerable<ISorterResultDiff> SorterResultDiffs { get; }
        bool AnySwitchDiffs { get; }
        bool AnySwitchResultDiffs { get; }
    }

    public class SorterResultPoolDiff
    {
        public static ISorterResultPoolDiff Make(IEnumerable<ISorterResult> sorterGroupA, IEnumerable<ISorterResult> sorterGroupB)
        {
            return new SorterResultPoolDiffImpl(sorterGroupA, sorterGroupB);
        }
    }

    public class SorterResultPoolDiffImpl : ISorterResultPoolDiff
    {
        public SorterResultPoolDiffImpl(IEnumerable<ISorterResult> sorterGroupA, IEnumerable<ISorterResult> sorterGroupB)
        {
            var sorterDictA = sorterGroupA.ToDictionary(s => s.Sorter.Guid);
            var sorterDictB = sorterGroupB.ToDictionary(s => s.Sorter.Guid);

            foreach (var sorter in sorterDictA.Values)
            {
                _sorterResultPoolDiff.Add
                    (
                        key: sorter.Sorter.Guid,
                        value:
                            SorterResultDiff.Make
                            (
                                sorter,
                                sorterDictB.ContainsKey(sorter.Sorter.Guid) ? sorterDictB[sorter.Sorter.Guid] : null
                            )
                    );

                if (sorterDictB.ContainsKey(sorter.Sorter.Guid))
                {
                    sorterDictB.Remove(sorter.Sorter.Guid);
                }
            }

            foreach (var sorter in sorterDictB.Values)
            {
                _sorterResultPoolDiff.Add
                (
                    key: sorter.Sorter.Guid,
                    value: SorterResultDiff.Make(sorter, null)
                );
            }
        }

        public bool AnySwitchDiffs
        {
            get
            {
                return SorterResultDiffs.Any(T => T.SwitchesAreDifferent);
            }
        }

        public bool AnySwitchResultDiffs
        {
            get
            {
                return SorterResultDiffs.Any(T => T.SwitchResultsAreDifferent);
            }
        }

        private readonly Dictionary<Guid, ISorterResultDiff> _sorterResultPoolDiff = new Dictionary<Guid, ISorterResultDiff>();

        public IEnumerable<ISorterResultDiff> SorterResultDiffs
        {
            get { return _sorterResultPoolDiff.Values; }
        }


    }
}
