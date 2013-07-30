using System.Windows;
using System.Windows.Controls;

namespace EquationEditor.Views.EquationAsText
{
    /// <summary>
    /// Interaction logic for TextFractionControl.xaml
    /// </summary>
    public partial class TextFractionControl : UserControl
    {
        public TextFractionControl()
        {
            InitializeComponent();
        }

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
            DependencyProperty.Register("ParenthesisVisibility", typeof(Visibility), typeof(TextFractionControl), new UIPropertyMetadata(Visibility.Collapsed));

        #endregion

        #region NumeratorTemplate

        public static DataTemplate GetNumeratorTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(NumeratorTemplateProperty);
        }

        public static void SetNumeratorTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(NumeratorTemplateProperty, value);
        }

        public static readonly DependencyProperty NumeratorTemplateProperty =
            DependencyProperty.Register("NumeratorTemplate", typeof(DataTemplate), typeof(TextFractionControl), new UIPropertyMetadata(null, BaseTemplateChanged));


        private static void BaseTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue==null)
            {

                var s = obj;
            }
        }

        #endregion


        #region DenominatorTemplate

        public static DataTemplate GetDenominatorTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DenominatorTemplateProperty);
        }

        public static void SetDenominatorTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DenominatorTemplateProperty, value);
        }

        public static readonly DependencyProperty DenominatorTemplateProperty =
            DependencyProperty.Register("DenominatorTemplate", typeof(DataTemplate), typeof(TextFractionControl), new UIPropertyMetadata(null));


        #endregion
    }
}
