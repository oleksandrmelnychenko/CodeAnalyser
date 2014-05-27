using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class BooleanToSelectionModeConverter : IValueConverter
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
                    return SelectionMode.Multiple;
                }
                return SelectionMode.Single;
            }
            catch (Exception)
            {
            }
            return Visibility.Collapsed;
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

