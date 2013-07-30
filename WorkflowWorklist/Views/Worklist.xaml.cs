using System.ComponentModel.Composition;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using WorkflowWorklist.ViewModels;

namespace WorkflowWorklist.Views
{
    /// <summary>
    /// Interaction logic for Worklist.xaml
    /// </summary>
    public partial class Worklist : UserControl, IContent, IPartImportsSatisfiedNotification
    {
       const string Spank = "Worklist";

        public Worklist()
        {
            InitializeComponent();
            //Tabby.Links
        }


        [Import(Spank)]
        public WorklistVm WorkListVm { get; set; }

        public void OnImportsSatisfied()
        {
            this.DataContext = this.WorkListVm;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

    }
}
