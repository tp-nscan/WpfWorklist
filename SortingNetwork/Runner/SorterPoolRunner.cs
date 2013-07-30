using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using SortingNetwork.SorterMonitors;
using SortingNetwork.Sorters;
using SortingNetwork.SwitchableMonitors;
using SortingNetwork.Switchables;

namespace SortingNetwork.Runner
{
    public class SorterPoolRunner
    {
        public SorterPoolRunner
          (
            IRando randomGenerator,
            double switchMutationRate,
            int sorterPopulationSize,
            int switchablePopulationSize,
            IEnumerable<ISorter> parentSorters,
            IEnumerable<ISwitchable> parentSwitchables
          )
        {
            _sorterPopulationSize = sorterPopulationSize;
            _switchablePopulationSize = switchablePopulationSize;
            _switchMutationRate = switchMutationRate;
            _randomGenerator = randomGenerator;

            var sorterPop = parentSorters.ToList();
            var switchablePop = parentSwitchables.ToList();

            _switchableParentPopulationSize = sorterPop.Count;
            _sorterParentPopulationSize = switchablePop.Count;

            if (SwitchableParentPopulationSize == 0)
            {
                throw new Exception("parent switchables are empty");
            }
            if (SorterParentPopulationSize == 0)
            {
                throw new Exception("parent sortables are empty");
            }

            sorterPop.AddRange
                (
                    sorterPop.ToList().SelectMany
                    (
                        T=>T.MutateSeveral
                            (
                                rndMutateOrNot: RandomGenerator.ToBool(SwitchMutationRate),
                                rndSwitchSelector: RandomGenerator.ToInt(),
                                sorterMaker: (guid, switches) => switches.ToSorter(guid),
                                copyNumber: SorterReproductionRate
                            )
                    )
                );

            switchablePop.AddRange
                (
                    switchablePop.ToList().SelectMany
                    (
                        T => T.CreateMutatedCopiesOf
                            (   
                               randomGen: RandomGenerator, 
                               copyNumber: SwitchableReproductionRate
                            )
                    )
                );


            _sorterMonitors = sorterPop.Select(T => T.Switches.ToSorterMonitor(T.Guid)).ToList();
            _switchableMonitors = switchablePop.Select(T => T.ToSwitchableMonitor(Guid.NewGuid())).ToList();
        }

        private readonly double _switchMutationRate;
        public double SwitchMutationRate
        {
            get { return _switchMutationRate; }
        }

        private readonly IRando _randomGenerator;
        public IRando RandomGenerator
        {
            get { return _randomGenerator; }
        }

        #region Sorter stuff

        private readonly int _sorterPopulationSize;
        public int SorterPopulationSize
        {
            get { return _sorterPopulationSize; }
        }

        readonly List<ISorterMonitor> _sorterMonitors = new List<ISorterMonitor>();
        public IEnumerable<ISorterMonitor> SorterMonitors
        {
            get { return _sorterMonitors; }
        }

        private readonly int _sorterParentPopulationSize;
        public int SorterParentPopulationSize
        {
            get { return _sorterParentPopulationSize; }
        }

        public int SorterChildPopulationSize
        {
            get { return SorterPopulationSize - SorterParentPopulationSize; }
        }

        public int SorterReproductionRate
        {
            get
            {
                return SorterChildPopulationSize / SorterParentPopulationSize;
            }
        }

        #endregion

        #region Switchable stuff

        private readonly int _switchablePopulationSize;
        public int SwitchablePopulationSize
        {
            get { return _switchablePopulationSize; }
        }

        private readonly List<ISwitchableMonitor> _switchableMonitors;
        public IEnumerable<ISwitchableMonitor> SwitchableMonitors
        {
            get { return _switchableMonitors; }
        }

        private readonly int _switchableParentPopulationSize;
        public int SwitchableParentPopulationSize
        {
            get { return _switchableParentPopulationSize; }
        }

        public int SwitchableChildPopulationSize
        {
            get { return SwitchablePopulationSize - SwitchableParentPopulationSize; }
        }

        public int SwitchableReproductionRate
        {
            get
            {
                return SwitchableChildPopulationSize / SwitchableParentPopulationSize;
            }
        }

        #endregion

        public IEnumerable<Tuple<ISorterMonitor, List<ISwitchableMonitor>>> TestInfo
        {
            get
            {
                return SorterMonitors
                    .Select(monitor => new Tuple<ISorterMonitor, List<ISwitchableMonitor>>
                        (
                            monitor,
                            _switchableMonitors
                        ));
            }
        }
    }
}
