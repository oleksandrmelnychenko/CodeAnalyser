using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
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
            try
            {
                bool valueOrDefault = false;
                if (value is bool)
                {
                    valueOrDefault = (bool) value;
                }
                else if (value is bool?)
                {
                    valueOrDefault = ((bool?) value).GetValueOrDefault();
                }
                if ((parameter != null) && bool.Parse((string) parameter))
                {
                    valueOrDefault = !valueOrDefault;
                }
                if (valueOrDefault)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
            return Visibility.Collapsed;
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            bool flag = (value is Visibility) && (((Visibility) value) == Visibility.Visible);
            if ((parameter != null) && ((bool) parameter))
            {
                flag = !flag;
            }
            return flag;
        }
    }
}

