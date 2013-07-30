using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SorterControls.Views.Entities
{
    /// <summary>
    /// Interaction logic for SwitchControl.xaml
    /// </summary>
    public partial class SwitchControl
    {
        public SwitchControl()
        {
          
            InitializeComponent();
        }

        //#region FontSize

        ///// <summary>
        ///// FontSize Dependency Property
        ///// </summary>
        //public static readonly DependencyProperty FontSizeProperty =
        //    DependencyProperty.Register("FontSize", typeof(double), typeof(SwitchControl),
        //        new FrameworkPropertyMetadata(10.0));

        ///// <summary>
        ///// Gets or sets the FontSize property. This dependency property 
        ///// indicates the height of each item.
        ///// </summary>
        //public double FontSize
        //{
        //    get { return (double)GetValue(FontSizeProperty); }
        //    set { SetValue(FontSizeProperty, value); }
        //}

        //#endregion

        #region Text

        /// <summary>
        /// Text Dependency Property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(double), typeof(SwitchControl),
                new FrameworkPropertyMetadata(10.0));

        /// <summary>
        /// Gets or sets the Text property. This dependency property 
        /// indicates the height of each item.
        /// </summary>
        public double Text
        {
            get { return (double)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion
    }

    public class SorterDesignData : ObservableCollection<SwitchData>
    {
        public SorterDesignData()
        {
            var b1 = new SolidColorBrush(Colors.Peru);
            b1.Freeze();
            var b2 = new SolidColorBrush(Colors.MediumSpringGreen);
            b2.Freeze();
            var b3 = new SolidColorBrush(Colors.MediumOrchid);
            b3.Freeze();
            var b4 = new SolidColorBrush(Colors.MediumBlue);
            b4.Freeze();
            var b5 = new SolidColorBrush(Colors.Yellow);
            b5.Freeze();

            Add(new SwitchData("2_11", b1, b2));
            Add(new SwitchData("3_7", b2, b3));
            Add(new SwitchData("12_11", b3, b4));
            Add(new SwitchData("14_6", b4, b5));
            Add(new SwitchData("6_8", b5, b1));
            Add(new SwitchData("15_3", b1, b3));
        }
    }

    public class SwitchData
    {
        public SwitchData(string text, Brush foregroundBrush, Brush backgroundBrush)
        {
            _text = text;
            _foregroundBrush = foregroundBrush;
            _backgroundBrush = backgroundBrush;
        }

        private readonly string _text;
        public string Text
        {
            get { return _text; }
        }

        private readonly Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
        }

        private readonly Brush _foregroundBrush;
        public Brush ForegroundBrush
        {
            get { return _foregroundBrush; }
        }
    }
}
