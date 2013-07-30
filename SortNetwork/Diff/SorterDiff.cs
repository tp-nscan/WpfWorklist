using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using SortNetwork.Sorters;

namespace SortNetwork.Diff
{
    public interface ISorterDiff
    {
        bool GuidsAreDifferent { get; }
        bool SwitchesAreDifferent { get; }
        ISorter SorterA { get; }
        ISorter SorterB { get; }
        IEnumerable<ISwitchDiff> SwitchDiffs { get; }
    }

    public static class SorterDiff
    {
        public static ISorterDiff Make(ISorter sorterA, ISorter sorterB)
        {
            return new SorterDiffImpl(sorterA, sorterB);
        }
    }

    class SorterDiffImpl : ISorterDiff
    {
        private readonly ISorter _sorterA;
        private readonly ISorter _sorterB;

        public SorterDiffImpl(ISorter sorterA, ISorter sorterB) : this(sorterA, sorterB, s=>s.Switches.OrderBy(T=>T.Index))
        {
            _sorterA = sorterA;
            _sorterB = sorterB;
        }

        protected SorterDiffImpl(ISorter sorterA, ISorter sorterB, Func<ISorter, IEnumerable<ISwitch>> switchSelector)
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
                        .Select((item, dex) => SwitchDiff.Make(dex, item.Item1, item.Item2))
                );
        }

        private readonly bool _oneSorterIsMissing;
        public bool OneSorterIsMissing
        {
            get { return _oneSorterIsMissing; }
        }

        public bool GuidsAreDifferent
        {
            get { return _sorterA.Guid != _sorterB.Guid; }
        }

        public bool SwitchesAreDifferent
        {
            get
            {
                if (OneSorterIsMissing)
                {
                    return true;
                }
                return _switchDiffs.Any(T => T.SwitchesAreDifferent);
            }
        }

        public ISorter SorterA
        {
            get { return _sorterA; }
        }

        public ISorter SorterB
        {
            get { return _sorterB; }
        }

        private readonly List<ISwitchDiff> _switchDiffs = new List<ISwitchDiff>();
        public IEnumerable<ISwitchDiff> SwitchDiffs
        {
            get { return _switchDiffs; }
        }
    }
}
