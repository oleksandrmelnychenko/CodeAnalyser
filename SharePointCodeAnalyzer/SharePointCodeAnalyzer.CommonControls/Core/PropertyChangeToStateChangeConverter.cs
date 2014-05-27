using System;
using System.Globalization;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class PropertyChangeToStateChangeConverter : IValueConverter
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
            if (value == null)
            {
                return (parameter + "|");
            }
            return (parameter + "|" + value.ToString());
        }
    }
}
