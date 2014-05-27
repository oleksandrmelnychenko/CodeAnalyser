using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class DoubleToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            double num = (double) value;
            if (parameter == null)
            {
                return new GridLength(num, GridUnitType.Star);
            }
            if (num <= 1.0)
            {
                return new GridLength(1.0 - num, GridUnitType.Star);
            }
            return new GridLength(100.0 - num, GridUnitType.Star);
        }
    }
}

