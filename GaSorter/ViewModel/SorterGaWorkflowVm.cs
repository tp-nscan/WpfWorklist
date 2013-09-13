using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DynamicModel.Common;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using DynamicModel.ViewModel.Workflow;
using Microsoft.Win32;
using Newtonsoft.Json;
using SorterControls.ViewModels.Bulders;
using SorterControls.ViewModels.Common;
using SorterControls.ViewModels.Entities;
using SortingNetworkDm.Entities;
using SortingNetworkDm.Json.Workflows;
using SortingNetworkDm.Workflows;
using WpfUtils;

namespace GaSorter.ViewModel
{
    public static class SorterGaWorkflowVm
    {
        public static bool FileWasSaved(this IWorkflow workflow)
        {
            return !String.IsNullOrEmpty(workflow.FilePath);
        }

        public static IWorkflowVm Make(ISorterWorkflow workflow, IEntityProvider entityProvider)
        {
            return new SorterGaWorkflowVmImpl(workflow, entityProvider);
        }

        public static IEntityVm ToEntityVm(this IEntity entity)
        {
            switch (entity.TypeName)
            {
                case SorterResultPoolEntity.TypeName:
                    return SorterResultPoolVm.Make((ISorterResultPoolEntity)entity);
                case SorterPoolEntity.TypeName:
                    return SorterPoolVm.Make((ISorterPoolEntity)entity);
                case SwitchablePoolEntity.TypeName:
                    return SwitchablePoolVm.Make((ISwitchablePoolEntity)entity);
            }
            throw new Exception(entity.TypeName + " not handled in MakeWorkflowStepVm");
        }
    }

    public class SorterGaWorkflowVmImpl : WorkflowVmImpl
    {
        public SorterGaWorkflowVmImpl(ISorterWorkflow workflow, IEntityProvider entityProvider)
            : base(workflow)
        {
            foreach (var modelEntity in Workflow.Entities)
            {
                EntityVms.Items.Add(item: MakeEntityVm(modelEntity));
            }

            foreach (var modelStep in Workflow.Steps)
            {
                StepVms.Items.Add(MakeStepVm(modelStep));
            }

            StepBuilderHostVm = SorterControls.ViewModels.Bulders.StepBuilderHostVm.Make(Workflow, entityProvider);
            StepBuilderHostVm.OnStepCreated.Subscribe(NewStep);
            BuilderIsSelected = true;
        }

        void NewStep(IStep step)
        {
            Workflow.AddStep(step);
            StepDetailsIsSelected = true;
            var newStepVm = StepVms.Items.Single(T => T.Step == step);
            StepVms.SelectedItem = newStepVm;
        }

        private bool _builderIsSelected;
        public bool BuilderIsSelected
        {
            get { return _builderIsSelected; }
            set
            {
                _builderIsSelected = value;
                OnPropertyChanged("BuilderIsSelected");
            }
        }

        private bool _stepDetailsIsSelected;
        public bool StepDetailsIsSelected
        {
            get { return _stepDetailsIsSelected; }
            set
            {
                _stepDetailsIsSelected = value;
                OnPropertyChanged("StepDetailsIsSelected");
            }
        }

        public ISorterWorkflow SorterWorkflow
        {
            get { return (ISorterWorkflow)Workflow; }
        }

        protected override IEntityVm MakeEntityVm(IEntity entity)
        {
            return entity.ToEntityVm();
        }

        protected override IStepVm MakeStepVm(IStep step)
        {
            return step.ToStepVm();
        }

        protected override bool CanCloseWorkflow()
        {
            return ! Dirty;
        }

        #region SaveWorkflow Command

        RelayCommand _saveWorkflowCommand;

        public override ICommand SaveWorkflow
        {
            get
            {
                return _saveWorkflowCommand ?? (_saveWorkflowCommand
                    = new RelayCommand
                        (
                            param => OnSaveWorkflow(),
                            param => CanSaveWorkflow()
                        ));
            }
        }

        void OnSaveWorkflow()
        {
            var intitialDirectory = string.Empty;
            if (Workflow.FileWasSaved())
            {
                intitialDirectory = Workflow.FilePath;
            }

            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                Title = "Save the workflow file",
                Filter = "Workflow file (.txt)|*.txt",
                InitialDirectory = intitialDirectory,
                FileName = Workflow.FileName
            };

            if (dlg.ShowDialog() == true)
            {
                using (var outfile = new StreamWriter(dlg.FileName))
                {
                    Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                    outfile.Write(JsonConvert.SerializeObject(SorterWorkflowToJson.ToJson(SorterWorkflow)));
                }

                Dirty = false;
            }
        }

        bool CanSaveWorkflow()
        {
            return Dirty;
        }


        #endregion // SaveWorkspace Command

        #region SaveWorkflowAs Command

        RelayCommand _saveWorkflowAsCommand;

        public override ICommand SaveWorkflowAs
        {
            get
            {
                return _saveWorkflowAsCommand ?? ( _saveWorkflowAsCommand
                    = new RelayCommand
                        (
                            param => OnSaveWorkflow(),
                            param => true
                        ));
            }
        }

        #endregion // SaveWorkflowAs Command

        private IStepBuilderHostVm _stepBuilderHostVm;
        public IStepBuilderHostVm StepBuilderHostVm
        {
            get { return _stepBuilderHostVm; }
            set
            {
                _stepBuilderHostVm = value;
                OnPropertyChanged("StepBuilderHostVm");
            }
        }

    }
}
