using System;
using System.Windows;
using GaSorter.View;
using GaSorter.ViewModel;

namespace GaSorter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ////FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            //// new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));


            var window = new MainWindow();
            var viewModel = new MainWindowVm();
            EventHandler handler = null;
            handler = delegate
            {
                viewModel.RequestClose -= handler;
                window.Close();
            };
            viewModel.RequestClose += handler;
            window.DataContext = viewModel;


            //var viewModel = new ObservableSelectableNames();
            //var window = new Window1 {DataContext = viewModel};


            window.Show();
        }
    }
}
