using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using SortingNetwork.Switches;

namespace SortingNetwork.Sorters
{
    public class SorterDiff
    {
        private readonly ISorter _sorterA;
        private readonly ISorter _sorterB;

        public SorterDiff(ISorter sorterA, ISorter sorterB) :this(sorterA, sorterB, s=>s.Switches.OrderBy(T=>T.Index))
        {
            _sorterA = sorterA;
            _sorterB = sorterB;
        }

        protected SorterDiff(ISorter sorterA, ISorter sorterB, Func<ISorter, IEnumerable<ISwitch>> switchSelector)
        {
            _sorterA = sorterA;
            _sorterB = sorterB;

            _switchDiffs.AddRange
                (
                    switchSelector(sorterA).OuterJoinWith(switchSelector(sorterA))
                        .ToList()
                        .Select((item, dex) => new SwitchDiff(dex, item.Item1, item.Item2))
                );
        }

        public bool GuidsAreDifferent
        {
            get { return _sorterA.Guid != _sorterB.Guid; }
        }

        private bool? _switchesAreDifferent;
        public bool SwitchesAreDifferent
        {
            get
            {
                if (!_switchesAreDifferent.HasValue)
                {
                    _switchesAreDifferent = _switchDiffs.Any(T => T.AreDifferent);
                }
                return _switchesAreDifferent.Value;
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

        private readonly List<SwitchDiff> _switchDiffs = new List<SwitchDiff>();
        public IEnumerable<SwitchDiff> SwitchDiffs
        {
            get { return _switchDiffs; }
        }
    }
}
