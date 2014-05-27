using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class ImageSourceConverter : IValueConverter
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
            if (!(targetType == typeof(ImageSource)))
            {
                return value;
            }
            if (value is string)
            {
                string uriString = (string) value;
                if (uriString.StartsWith("http"))
                {
                    return uriString;
                }
                return new BitmapImage(new Uri(uriString, UriKind.RelativeOrAbsolute));
            }
            if (!(value is Uri))
            {
                return value;
            }
            Uri uriSource = (Uri) value;
            if (uriSource.ToString().StartsWith("http"))
            {
                return uriSource.ToString();
            }
            return new BitmapImage(uriSource);
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

