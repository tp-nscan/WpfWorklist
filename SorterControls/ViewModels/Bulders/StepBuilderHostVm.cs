using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using DynamicModel.Common;
using DynamicModel.Model;
using SortNetwork.Sorters;
using SortingNetworkDm.Entities;
using WpfUtils;

namespace SorterControls.ViewModels.Bulders
{
    public interface IStepBuilderHostVm
    {
        IObservable<IStep> OnStepCreated { get; }
    }

    public static class StepBuilderHostVm
    {
        public static IStepBuilderHostVm Make(IIndexProvider myIndexProvider, IEntityProvider entityProvider)
        {
            return new StepBuilderHostVmImpl(myIndexProvider, entityProvider);
        }
    }

    public class StepBuilderHostVmImpl : ViewModelBase, IStepBuilderHostVm
    {
        public StepBuilderHostVmImpl(IIndexProvider myIndexProvider, IEntityProvider entityProvider)
        {
            _myIndexProvider = myIndexProvider;
            _entityProvider = entityProvider;
            WorkflowStepBuilderVm = new EmptyStepBuilderVm();
        }

        private readonly IEntityProvider _entityProvider;
        public IEntityProvider EntityProvider
        {
            get { return _entityProvider; }
        }

        private IWorkflowStepBuilderVm _workflowStepBuilderVm;
        public IWorkflowStepBuilderVm WorkflowStepBuilderVm
        {
            get { return _workflowStepBuilderVm; }
            private set
            {
                _workflowStepBuilderVm = value;

                if (_newModelStepSubscription != null)
                {
                    _newModelStepSubscription.Dispose();
                }
                _newModelStepSubscription =_workflowStepBuilderVm.OnModelStepCreated.Subscribe(NewModelStep);

                OnPropertyChanged("WorkflowStepBuilderVm");
            }
        }

        private IDisposable _newModelStepSubscription;
        void NewModelStep(IStep step)
        {
            WorkflowStepBuilderVm = new EmptyStepBuilderVm();
            _stepCreated.OnNext(step);
        }


        private readonly Subject<IStep> _stepCreated = new Subject<IStep>();
        public IObservable<IStep> OnStepCreated
        {
            get { return _stepCreated; }
        }


        private readonly IIndexProvider _myIndexProvider;
        public IIndexProvider MyIndexProvider
        {
            get { return _myIndexProvider; }
        }

        #region RandSorters Command

        RelayCommand _randSorters;

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workflowStep from the user interface.
        /// </summary>
        public ICommand RandSortersCommand
        {
            get
            {
                return _randSorters ?? (_randSorters
                    = new RelayCommand
                        (
                            param => OnRandSortersStep(),
                            param => CanRandSortersStep()
                        ));
            }
        }


        void OnRandSortersStep()
        {
            WorkflowStepBuilderVm = new SorterPoolBuilderVm
            (
                indexProvider: MyIndexProvider
            );
        }

        bool CanRandSortersStep()
        {
            return true;
        }

        #endregion // RandSorters Command

        #region RandSwitchables Command

        RelayCommand _randSwitchables;

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workflowStep from the user interface.
        /// </summary>
        public ICommand RandSwitchablesCommand
        {
            get
            {
                return _randSwitchables ?? (_randSwitchables
                    = new RelayCommand
                        (
                            param => OnRandSwitchablesStep(),
                            param => CanRandSwitchablesStep()
                        ));
            }
        }


        void OnRandSwitchablesStep()
        {
            WorkflowStepBuilderVm = new SwitchablePoolBuilderVm
            (
                indexProvider: MyIndexProvider
            );
        }

        bool CanRandSwitchablesStep()
        {
            return true;
        }


        #endregion // RandSwitchables Command

        #region CompPool Command

        RelayCommand _compPool;
        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workflowStep from the user interface.
        /// </summary>
        public ICommand CompPoolCommand
        {
            get
            {
                return _compPool ?? (_compPool
                    = new RelayCommand
                        (
                            param => OnCompPoolStep(),
                            param => CanCompPoolStep()
                        ));
            }
        }

        void OnCompPoolStep()
        {
            WorkflowStepBuilderVm = new CompetePoolBuilderVm
            (
                indexProvider: MyIndexProvider,
                sorterPools: InputSorterPools,
                switchablePoolVms: EntityProvider.Entities.Where(T => T.TypeName == SwitchablePoolEntity.TypeName).Cast<ISwitchablePoolEntity>()
            );
        }

        IEnumerable<ISorterPoolEntity> InputSorterPools
        {
            get
            {
                foreach (var entity in EntityProvider.Entities)
                {
                    if (entity.TypeName == SorterPoolEntity.TypeName)
                    {
                        yield return (ISorterPoolEntity) entity;
                    }
                    if (entity.TypeName == SorterResultPoolEntity.TypeName)
                    {
                        var smpe = (ISorterResultPoolEntity)entity;
                        var spe = SorterPoolEntity.Make
                            (
                                  guid: smpe.Guid, 
                                  name: smpe.Name, 
                                  description: smpe.Description, 
                                  sorterRepo: smpe.SorterResultRepo.Select(T=>T.Sorter).ToSorterRepo()
                             );

                        yield return spe;
                    }
                }
            }
        }

        bool CanCompPoolStep()
        {
            return true;
        }

        #endregion // CompPool Command

    }
}
