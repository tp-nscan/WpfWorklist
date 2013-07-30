using System.Windows;
using WpfUtils.SelectableCollection.TestData;

namespace GaSorter.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var dspvm = new DesignSorterResultPoolVm();
            //ralph.SorterResultPoolVm = dspvm;
            var dc = DataContext as ObservableSelectableNames;
            if (dc != null) dc.SelectedItem = dc.Items[1];
        }
    }
}
