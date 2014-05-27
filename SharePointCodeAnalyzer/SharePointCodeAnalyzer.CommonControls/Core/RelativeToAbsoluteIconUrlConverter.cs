using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class RelativeToAbsoluteIconUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.InternalConvertBack(value, targetType, parameter);
        }

        private string GetResourceExists(string imageName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            ResourceManager manager = new ResourceManager(executingAssembly.FullName.Split(new char[] { ',' })[0].Trim() + ".g", executingAssembly);
            try
            {
                foreach (DictionaryEntry entry in manager.GetResourceSet(currentCulture, true, true))
                {
                    if (entry.Key.ToString().EndsWith(imageName))
                    {
                        return entry.Key.ToString();
                    }
                }
            }
            finally
            {
                manager.ReleaseAllResources();
            }
            return null;
        }

        private object InternalConvert(object value, Type targetType, object parameter)
        {
            string imageName = value.ToString();
            if (parameter != null)
            {
                try
                {
                    //ImageSize? nullable = Enum.Parse(typeof(ImageSize), parameter.ToString(), true) as ImageSize?;
                    //ImageSize valueOrDefault = nullable.GetValueOrDefault();
                    //if (nullable.HasValue)
                    //{
                    //    switch (valueOrDefault)
                    //    {
                    //        case ImageSize.Size32:
                    //            imageName = imageName + "_32";
                    //            goto Label_0093;

                    //        case ImageSize.Size48:
                    //            imageName = imageName + "_48";
                    //            goto Label_0093;

                    //        case ImageSize.Size64:
                    //            imageName = imageName + "_64";
                    //            goto Label_0093;

                    //        case ImageSize.Size128:
                    //            imageName = imageName + "_128";
                    //            goto Label_0093;
                    //    }
                    //}
                }
                catch (Exception)
                {
                }
            }
        Label_0093:
            if (!imageName.EndsWith(".png"))
            {
                imageName = imageName + ".png";
            }
            imageName = imageName.ToLower();
            string resourceExists = this.GetResourceExists(imageName);
            if (resourceExists == null)
            {
                return null;
            }
            string[] strArray = Assembly.GetExecutingAssembly().FullName.Split(new char[] { ',' });
            return new Uri("/" + strArray[0].Trim() + ";component/" + resourceExists, UriKind.RelativeOrAbsolute);
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

