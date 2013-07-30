using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Lab.ViewModel;

namespace Lab.Views
{

    public class KeyValuePairVmsConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var wellVm = value as WellVm;
            if(wellVm==null)
            {
                return DependencyProperty.UnsetValue;
            }
            return wellVm.KeyValuePairVms;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
