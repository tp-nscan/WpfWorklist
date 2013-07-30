using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using DynamicModel.Model;
using MathUtils.Collections;
using Microsoft.Win32;
using Newtonsoft.Json;
using SortingNetworkDm.Json.Workflows;
using SortingNetworkDm.Workflows;
using WorkflowWorklist.Models;
using WpfUtils;

namespace GaSorter.ViewModel
{
    public class MainWindowVm : ViewModelBase
{
    public MainWindowVm()
    {
        _sorterGaWorkflowGroupVm = ViewModel.SorterGaWorkflowGroupVm.Make(WorkflowGroup.Make());
    }

    private readonly ISorterGaWorkflowGroupVm _sorterGaWorkflowGroupVm;
    public ISorterGaWorkflowGroupVm SorterGaWorkflowGroupVm
    {
        get { return _sorterGaWorkflowGroupVm; }
    }

    private readonly Worklist _worklist = new Worklist();
    public Worklist Worklist
    {
        get { return _worklist; }
    }

    #region NewWorkflow Command

    RelayCommand _newWorkflowCommand;

    public ICommand NewWorkflow
    {
        get
        {
            return _newWorkflowCommand ?? (_newWorkflowCommand
                = new RelayCommand
                    (
                        param => OnNewWorkflow(),
                        param => CanNewWorkflow()
                    ));
        }
    }


    protected void OnNewWorkflow()
    {
        var newWorkflow = SorterWorkflow.Create
            (
                name: Enumerable.Range(1, 100).Select(T => "[New " + T + " ]")
                                .GetFirstNonMatching
                                    (
                                        values: SorterGaWorkflowGroupVm.WorkflowGroup.Workflows,
                                        keySelector: wf => wf.FileName
                                    )
            );
        
        SorterGaWorkflowGroupVm.WorkflowGroup.AddWorkflow(newWorkflow);

        SorterGaWorkflowGroupVm.WorkflowVms
            .MoveCurrentTo(SorterGaWorkflowGroupVm.WorkflowVms.Single(T=>T.Workflow == newWorkflow));
    }

    bool CanNewWorkflow()
    {
        return true;
    }


    #endregion // NewWorkflow Command

    #region OpenWorkflow Command

    RelayCommand _openWorkflowCommand;

    public ICommand OpenWorkflow
    {
        get
        {
            return _openWorkflowCommand ?? (_openWorkflowCommand
                = new RelayCommand
                    (
                        param => OnOpenWorkflow(),
                        param => CanOpenWorkflow()
                    ));
        }
    }


    void OnOpenWorkflow()
    {

        var dlg = new OpenFileDialog
        {
            Title = "Select the workspace file",
            Filter =  String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", "txt", "workspace"),
            //"workspace file (*.txt)|*.txt",
            InitialDirectory = string.Empty
        };

        if (dlg.ShowDialog() == true)
        {
            string fileData = string.Empty;
            try
            {
                using (var streamReader = new StreamReader(dlg.FileName))
                {
                    fileData = streamReader.ReadToEnd();
                   
                }
            }
            catch (Exception)
            {
                throw;
            }
            var deserialized = JsonConvert.DeserializeObject<SorterWorkflowToJson>(fileData);

            var openedWorkflow = SorterWorkflowToJson.ToSorterWorkflow(deserialized);
            SorterGaWorkflowGroupVm.WorkflowGroup.AddWorkflow(openedWorkflow);

            SorterGaWorkflowGroupVm.WorkflowVms
                .MoveCurrentTo(SorterGaWorkflowGroupVm.WorkflowVms.Single(T => T.Workflow == openedWorkflow));
        }
    }

    bool CanOpenWorkflow()
    {
        return true;
    }


    #endregion // OpenWorkflow Command

    #region SaveWorkflow Command

    RelayCommand _saveWorkflowCommand;

    public ICommand SaveWorkflow
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
        var selectedVm = SorterGaWorkflowGroupVm.WorkflowVms.SelectedItem();
        if(selectedVm == null) return;

        selectedVm.SaveWorkflow.Execute(string.Empty);
    }

    bool CanSaveWorkflow()
    {
        var selectedVm = SorterGaWorkflowGroupVm.WorkflowVms.SelectedItem();
        return (selectedVm != null) && (selectedVm.Dirty);
    }

    #endregion // SaveWorkflow Command

    #region SaveWorkflowAs Command

    RelayCommand _saveWorkflowAsCommand;

    public ICommand SaveWorkflowAs
    {
        get
        {
            return _saveWorkflowAsCommand ?? (_saveWorkflowAsCommand
                = new RelayCommand
                    (
                        param => OnSaveWorkflow(),
                        param => SorterGaWorkflowGroupVm.WorkflowVms.SelectedItem() != null
                    ));
        }
    }

    #endregion // SaveWorkflowAs Command

    #region CloseCommand

    private RelayCommand _closeCommand;
        /// <summary>
    /// Returns the command that, when invoked, attempts
    /// to remove this workspace from the user interface.
    /// </summary>
    public ICommand CloseCommand
    {
        get
        {
            if (_closeCommand == null)
                _closeCommand = new RelayCommand(param => OnRequestClose());

            return _closeCommand;
        }
    }

    #endregion // CloseCommand

    #region RequestClose [event]

    /// <summary>
    /// Raised when this workspace should be removed from the UI.
    /// </summary>
    public event EventHandler RequestClose;

    private void OnRequestClose()
    {
        EventHandler handler = RequestClose;
        if (handler != null)
            handler(this, EventArgs.Empty);
    }

    #endregion // RequestClose [event]
}
}
