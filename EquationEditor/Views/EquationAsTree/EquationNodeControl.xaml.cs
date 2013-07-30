using System.Windows;

namespace EquationEditor.Views.EquationAsTree
{
    /// <summary>
    /// Interaction logic for EquationNodeControl.xaml
    /// </summary>
    public partial class EquationNodeControl
    {
        public EquationNodeControl()
        {
            InitializeComponent();
        }

        #region BaseTemplate

        public static DataTemplate GetBaseTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(BaseTemplateProperty);
        }

        public static void SetBaseTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(BaseTemplateProperty, value);
        }

        public static readonly DependencyProperty BaseTemplateProperty =
            DependencyProperty.Register("BaseTemplate", typeof(DataTemplate), typeof(EquationNodeControl), new UIPropertyMetadata(null, BaseTemplateChanged));


        private static void BaseTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var s = obj;
        }

        #endregion

        #region ContentTemplateName

        public static string GetContentTemplateName(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentTemplateNameProperty);
        }

        public static void SetContentTemplateName(DependencyObject obj, string value)
        {
            obj.SetValue(ContentTemplateNameProperty, value);
        }

        public static readonly DependencyProperty ContentTemplateNameProperty =
            DependencyProperty.Register("ContentTemplateName", typeof(string), typeof(EquationNodeControl),
            new UIPropertyMetadata(string.Empty, ContentTemplateNameChanged));

        private static void ContentTemplateNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var treeControl = obj as EquationNodeControl;
            var controlName = e.NewValue as string;

            if((treeControl != null) & !string.IsNullOrEmpty(controlName))
            {
                var template = treeControl.Resources[controlName];
                if (template != null)
                {
                    treeControl.contentPresenter.ContentTemplate = (DataTemplate)template;
                    return;
                }
                treeControl.contentPresenter.ContentTemplate = (DataTemplate)treeControl.Resources["unknown"];
            }
        }

        #endregion
    }
}
