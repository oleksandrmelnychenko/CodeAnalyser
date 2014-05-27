using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class ViewModelToViewConverter : IValueConverter
    {
        private Dictionary<string, DependencyObject> cache = new Dictionary<string, DependencyObject>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return this.InternalConvert(value, targetType, parameter);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            if (value == null)
            {
                return null;
            }
            try
            {
                string key = value.GetType().ToString();
                DependencyObject viewForViewModel = null;
                if (this.cache.ContainsKey(key))
                {
                    viewForViewModel = this.cache[key];
                }
                else
                {
                    //viewForViewModel = IoCContainer.GetViewForViewModel(value.GetType()) as DependencyObject;
                    //if (viewForViewModel == null)
                    //{
                    //    viewForViewModel = IoCContainer.GetViewForViewModel(value.GetType().GetTypeInfo().BaseType) as DependencyObject;
                    //}
                    //if (viewForViewModel == null)
                    //{
                    //    viewForViewModel = IoCContainer.GetViewForViewModel(value.GetType().GetTypeInfo().BaseType.GetTypeInfo().BaseType) as DependencyObject;
                    //}
                    //if (viewForViewModel == null)
                    //{
                    //    viewForViewModel = IoCContainer.GetViewForViewModel(value.GetType().GetTypeInfo().BaseType.GetTypeInfo().BaseType.GetTypeInfo().BaseType) as DependencyObject;
                    //}
                    this.cache.ContainsKey(key);
                }
                if (viewForViewModel is FrameworkElement)
                {
                    (viewForViewModel as FrameworkElement).DataContext = value;
                }
                return viewForViewModel;
            }
            catch (Exception)
            {
            }
            return value;
        }
    }
}

