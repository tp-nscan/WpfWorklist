using System.Windows;
using System.Windows.Controls;

namespace EquationEditor.Views.EquationAsText
{
    /// <summary>
    /// Interaction logic for TextPowerControl.xaml
    /// </summary>
    public partial class TextPowerControl : UserControl
    {
        public TextPowerControl()
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
            DependencyProperty.Register("BaseTemplate", typeof(DataTemplate), typeof(TextPowerControl), new UIPropertyMetadata(null, BaseTemplateChanged));


        private static void BaseTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var s = obj;
        }

        #endregion

        #region PowerTemplate

        public static DataTemplate GetPowerTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(PowerTemplateProperty);
        }

        public static void SetPowerTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(PowerTemplateProperty, value);
        }

        public static readonly DependencyProperty PowerTemplateProperty =
            DependencyProperty.Register("PowerTemplate", typeof(DataTemplate), typeof(TextPowerControl), new UIPropertyMetadata(null, PowerTemplateChanged));


        private static void PowerTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                var s = obj;
            }
        }

        #endregion

        #region ParenthesisVisibility

        public static Visibility GetParenthesisVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(ParenthesisVisibilityProperty);
        }

        public static void SetParenthesisVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(ParenthesisVisibilityProperty, value);
        }

        public static readonly DependencyProperty ParenthesisVisibilityProperty =
            DependencyProperty.Register("ParenthesisVisibility", typeof(Visibility), typeof(TextPowerControl), new UIPropertyMetadata(Visibility.Collapsed));

        #endregion
    }
}
