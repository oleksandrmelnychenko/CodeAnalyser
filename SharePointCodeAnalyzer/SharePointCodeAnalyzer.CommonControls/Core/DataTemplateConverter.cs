using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class DataTemplateConverter : IValueConverter
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
                str = parameter.ToString();
            }
            Type typeOfDataContext = value.GetType();
            //bool flag1 = typeOfDataContext == typeof(VisitorViewModel);
            //if ((value is ContentPresenter) && ((value as ContentPresenter).Content != null))
            //{
            //    BaseViewModel content = (value as ContentPresenter).Content as BaseViewModel;
            //    typeOfDataContext = (value as ContentPresenter).Content.GetType();
            //}
            return (ResourceHelper.GetResourceOfType(typeOfDataContext, typeof(Style), str + "_", "") as DataTemplate);
        }
    }
}
