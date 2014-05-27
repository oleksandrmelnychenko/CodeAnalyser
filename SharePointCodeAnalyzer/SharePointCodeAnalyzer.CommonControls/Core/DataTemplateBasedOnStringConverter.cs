using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class DataTemplateBasedOnStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object InternalConvert(object value, Type targetType, object parameter)
        {
            string str = "";
            if (value == null)
            {
                return null;
            }
            if (parameter != null)
            {
                str = parameter.ToString() + "_";
            }
            return (ResourceHelper.FindResource(str + value.ToString()) as DataTemplate);
        }
    }
}

