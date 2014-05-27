using System;
using System.Globalization;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class StringToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvertBack(value, targetType, parameter);
        }

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            return ((string) value).ToUpper();
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            return value;
        }
    }
}

