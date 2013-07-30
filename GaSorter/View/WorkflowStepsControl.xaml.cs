using System.Windows;
using DynamicModel.ViewModel;
using SorterControls.ViewModels.Steps;

namespace SorterControls.Views.Steps
{
    /// <summary>
    /// Interaction logic for WorkflowStepsControl.xaml
    /// </summary>
    public partial class WorkflowStepsControl
    {
        public WorkflowStepsControl()
        {
            InitializeComponent();
        }

        #region SelectedStep

        /// <summary>
        /// Rotation Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedStepProperty =
            DependencyProperty.Register("SelectedStep", typeof(IStepVm),
                typeof(WorkflowStepsControl), new UIPropertyMetadata(SelectedStepChanged));

        public IStepVm SelectedStep
        {
            get { return (IStepVm)GetValue(SelectedStepProperty); }
            set { SetValue(SelectedStepProperty, value); }
        }

        private static void SelectedStepChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion
    }
}
