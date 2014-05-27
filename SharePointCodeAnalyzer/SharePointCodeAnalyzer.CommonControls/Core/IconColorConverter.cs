using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class IconColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvertBack(value, targetType, parameter);
        }

        private Brush GetColorForType(BaseViewModel dataContext)
        {
            string name = dataContext.GetType().Name;
            Brush colorForType = ResourceHelper.FindResource("Brush_" + name) as Brush;
            //if ((colorForType == null) && (dataContext.Parent != null))
            //{
            //    colorForType = this.GetColorForType(dataContext.Parent);
            //}
            return colorForType;
        }

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            try
            {
                object dataContext = value;
                if (value is FrameworkElement)
                {
                    dataContext = (value as FrameworkElement).DataContext;
                }
                if (dataContext != null)
                {
                    //if ((dataContext is BaseItemViewModel) && ((dataContext as BaseItemViewModel).Parent != null))
                    //{
                    //    dataContext = (dataContext as BaseItemViewModel).Parent;
                    //}
                    //Brush colorForType = ResourceHelper.GetResourceOfType(dataContext.GetType(), typeof(Brush), "Brush_", null) as Brush;
                    //if (((colorForType == null) && (dataContext is BaseViewModel)) && ((dataContext as BaseViewModel).Parent != null))
                    //{
                    //    colorForType = this.GetColorForType((dataContext as BaseViewModel).Parent);
                    //}
                    //if (colorForType != null)
                    //{
                    //    return colorForType;
                    //}
                }
            }
            catch (Exception)
            {
            }
            if (parameter != null)
            {
                try
                {
                    return new SolidColorBrush();
                }
                catch
                {
                }
            }
            return new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0, 0));
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            return null;
        }
    }
}

