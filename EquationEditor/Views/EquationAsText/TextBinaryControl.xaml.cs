using System.Windows;
using System.Windows.Controls;

namespace EquationEditor.Views.EquationAsText
{
    /// <summary>
    /// Interaction logic for TextBinaryControl.xaml
    /// </summary>
    public partial class TextBinaryControl : UserControl
    {
        public TextBinaryControl()
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
            DependencyProperty.Register("ParenthesisVisibility", typeof(Visibility), typeof(TextBinaryControl), new UIPropertyMetadata(Visibility.Collapsed));

        #endregion

        #region OperatorText

        public static string GetOperatorText(DependencyObject obj)
        {
            return (string)obj.GetValue(OperatorTextProperty);
        }

        public static void SetOperatorText(DependencyObject obj, string value)
        {
            obj.SetValue(OperatorTextProperty, value);
        }

        public static readonly DependencyProperty OperatorTextProperty =
            DependencyProperty.Register("OperatorText", typeof(string), typeof(TextBinaryControl), new UIPropertyMetadata("+"));

        #endregion

        #region LeftTemplate

        public static DataTemplate GetLeftTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(LeftTemplateProperty);
        }

        public static void SetLeftTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(LeftTemplateProperty, value);
        }

        public static readonly DependencyProperty LeftTemplateProperty =
            DependencyProperty.Register("LeftTemplate", typeof(DataTemplate), typeof(TextBinaryControl), new UIPropertyMetadata(null, BaseTemplateChanged));


        private static void BaseTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var s = obj;
        }

        #endregion


        #region RightTemplate

        public static DataTemplate GetRightTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(RightTemplateProperty);
        }

        public static void SetRightTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(RightTemplateProperty, value);
        }

        public static readonly DependencyProperty RightTemplateProperty =
            DependencyProperty.Register("RightTemplate", typeof(DataTemplate), typeof(TextBinaryControl), new UIPropertyMetadata(null));


        #endregion
    }
}
