using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using SortNetwork.Results;

namespace SortNetwork.Diff
{
    public interface ISorterResultDiff
    {
        bool GuidsAreDifferent { get; }
        bool SwitchesAreDifferent { get; }
        bool SwitchResultsAreDifferent { get; }
        ISorterResult SorterResultA { get; }
        ISorterResult SorterResultB { get; }
        IEnumerable<ISwitchResultDiff> SwitchResultDiffs { get; }
    }

    public static class SorterResultDiff
    {
        public static ISorterResultDiff Make(ISorterResult sorterA, ISorterResult sorterB)
        {
            return new SorterResultDiffImpl(sorterA, sorterB);
        }
    }

    class SorterResultDiffImpl : ISorterResultDiff
    {
        private readonly ISorterResult _sorterA;
        private readonly ISorterResult _sorterB;

        public SorterResultDiffImpl(ISorterResult sorterA, ISorterResult sorterB)
            : this(sorterA, sorterB, s => s.SwitchResults.OrderBy(T => T.Index))
        {
            _sorterA = sorterA;
            _sorterB = sorterB;
        }

        protected SorterResultDiffImpl
            (ISorterResult sorterA, ISorterResult sorterB, Func<ISorterResult, IEnumerable<ISwitchResult>> switchSelector)
        {
            _sorterA = sorterA;
            _sorterB = sorterB;

            if ((_sorterA == null) || (_sorterB == null))
            {
                _oneSorterIsMissing = true;
                return;
            }

            _switchDiffs.AddRange
                (
                    switchSelector(sorterA).OuterJoinWith(switchSelector(sorterB))
                        .ToList()
                        .Select((item, dex) => SwitchResultDiff.Make(dex, item.Item1, item.Item2))
                );
        }

        private readonly bool _oneSorterIsMissing;
        public bool OneSorterIsMissing
        {
            get { return _oneSorterIsMissing; }
        }

        public bool GuidsAreDifferent
        {
            get { return _sorterA.Sorter.Guid != _sorterB.Sorter.Guid; }
        }

        public bool SwitchesAreDifferent
        {
            get
            {
                if (OneSorterIsMissing)
                {
                    return true;
                }
                return _switchDiffs.Any ( T => T.SwitchesAreDifferent );
            }
        }

        public bool SwitchResultsAreDifferent
        {
            get
            {
                if (OneSorterIsMissing)
                {
                    return true;
                }
                return _switchDiffs.Any
                        (
                            T => T.SwitchesAreDifferent
                                    ||
                                 T.UsagesAreDifferent
                        );
            }
        }

        public ISorterResult SorterResultA
        {
            get { return _sorterA; }
        }

        public ISorterResult SorterResultB
        {
            get { return _sorterB; }
        }

        private readonly List<ISwitchResultDiff> _switchDiffs = new List<ISwitchResultDiff>();

        public IEnumerable<ISwitchResultDiff> SwitchResultDiffs
        {
            get { return _switchDiffs; }
        }
    }
}
