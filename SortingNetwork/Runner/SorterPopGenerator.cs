using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using SortingNetwork.SorterMonitors;
using SortingNetwork.Sorters;
using SortingNetwork.Switchables;

namespace SortingNetwork.Runner
{
    public class SorterPopGenerator
    {
        public SorterPopGenerator
          (
            IRandomInt randomForKeyPairs,
            IGenerator<bool> mutateOrNot, 
            int sorterPopulationSize,
            int switchablePopulationSize,
            IEnumerable<ISorter> parentMonitors, 
            ISwitchableRepo<ISwitchable> switchableRepo
          )
        {
            _sorterPopulationSize = sorterPopulationSize;
            _switchablePopulationSize = switchablePopulationSize;
            _switchableRepo = switchableRepo;
            _randomForKeyPairs = randomForKeyPairs;
            _mutateOrNot = mutateOrNot;
            _parentMonitors = parentMonitors.Select(T => T.Switches.ToSorterMonitor(T.Guid)).ToList();

            if(_parentMonitors.Count==0) return;

            _childMonitors = _parentMonitors.Select
                (
                    T => T.MutateToSorterMonitors
                             (
                                 rndMutateOrNot: MutateOrNot, 
                                 rndSwitchSelector: RandomForKeyPairs,
                                 copyNumber: ReproductionRate
                             )
                ).SelectMany(T=>T).ToList();
        }

        public int ChildPopulationSize
        {
            get { return SorterPopulationSize - ParentPopulationSize; }
        }

        public int ParentPopulationSize
        {
            get { return _parentMonitors.Count; }
        }

        public int ReproductionRate
        {
            get 
            { 
                return ChildPopulationSize / ParentPopulationSize; 
            }
        }

        private readonly IRandomInt _randomForKeyPairs;
        IRandomInt RandomForKeyPairs
        {
            get { return _randomForKeyPairs; }
        }

        private readonly IGenerator<bool> _mutateOrNot;
        IGenerator<bool> MutateOrNot
        {
            get { return _mutateOrNot; }
        }

        private readonly int _sorterPopulationSize;
        public int SorterPopulationSize
        {
            get { return _sorterPopulationSize; }
        }

        private readonly int _switchablePopulationSize;
        public int SwitchablePopulationSize
        {
            get { return _switchablePopulationSize; }
        }

        readonly List<ISorterMonitor> _childMonitors = new List<ISorterMonitor>();
        public IEnumerable<ISorterMonitor> ChildMonitors
        {
            get { return _childMonitors; }
        }

        readonly List<ISorterMonitor> _parentMonitors = new List<ISorterMonitor>();
        public IEnumerable<ISorterMonitor> ParentMonitors
        {
            get { return _parentMonitors; }
        }

        private readonly ISwitchableRepo<ISwitchable> _switchableRepo;
        public ISwitchableRepo<ISwitchable> SwitchableRepo
        {
            get { return _switchableRepo; }
        }

        public IEnumerable<Tuple<ISorterMonitor, ISwitchableRepo<ISwitchable>>> TestInfo
        {
            get
            {
                foreach (var monitor in ParentMonitors)
                {
                    yield return new Tuple<ISorterMonitor, ISwitchableRepo<ISwitchable>>
                        (
                            monitor,
                            SwitchableRepo
                        );
                }

                foreach (var monitor in ChildMonitors)
                {
                    yield return new Tuple<ISorterMonitor, ISwitchableRepo<ISwitchable>>
                        (
                            monitor,
                            SwitchableRepo
                        );
                }
            }
        }
    }
}
