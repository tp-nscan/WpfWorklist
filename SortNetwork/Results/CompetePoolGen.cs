using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using SortNetwork.Sorters;
using SortNetwork.Switchables;

namespace SortNetwork.Results
{
    public class CompetePoolGen
    {
        public CompetePoolGen
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

            _switchableParentPopulationSize = switchablePop.Count;
            _sorterParentPopulationSize = sorterPop.Count;

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
                        T => T.MutateSeveral
                            (
                                rndMutateOrNot: RandomGenerator.ToBool(SwitchMutationRate),
                                rndSwitchSelector: RandomGenerator.ToInt(),
                                rndIdMaker: RandomGenerator.ToGuid(),
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


            _sorterPop = sorterPop;
            _switchablePop = switchablePop;
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

        readonly List<ISorter> _sorterPop = new List<ISorter>();
        public IEnumerable<ISorter> SorterPop
        {
            get { return _sorterPop; }
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

        private readonly List<ISwitchable> _switchablePop;
        public IEnumerable<ISwitchable> SwitchablePop
        {
            get { return _switchablePop; }
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

        public IEnumerable<Tuple<ISorter, List<ISwitchable>>> TestInfo
        {
            get
            {
                return SorterPop
                    .Select(monitor => new Tuple<ISorter, List<ISwitchable>>
                        (
                            monitor,
                            _switchablePop
                        ));
            }
        }
    }
}
