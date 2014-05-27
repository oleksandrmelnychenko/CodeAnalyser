using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public sealed class TypeToImageConverter : IValueConverter
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
            ResourceManager manager = new ResourceManager(Assembly.GetExecutingAssembly().FullName.Split(new char[] { ',' })[0].Trim() + ".g", executingAssembly);
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
            try
            {
                string imageName = value.GetType().Name.ToString().Replace("ViewModel", "");
                if (parameter != null)
                {
                    try
                    {
                        ImageSize? nullable = Enum.Parse(typeof(ImageSize), parameter.ToString(), true) as ImageSize?;
                        ImageSize valueOrDefault = nullable.GetValueOrDefault();
                        if (nullable.HasValue)
                        {
                            switch (valueOrDefault)
                            {
                                case ImageSize.Size32:
                                    imageName = imageName + "_32";
                                    goto Label_00AE;

                                case ImageSize.Size48:
                                    imageName = imageName + "_48";
                                    goto Label_00AE;

                                case ImageSize.Size64:
                                    imageName = imageName + "_64";
                                    goto Label_00AE;

                                case ImageSize.Size128:
                                    imageName = imageName + "_128";
                                    goto Label_00AE;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            Label_00AE:
                imageName = (imageName + ".png").ToLower();
                string resourceExists = this.GetResourceExists(imageName);
                if (resourceExists == null)
                {
                    return null;
                }
                string[] strArray = Assembly.GetExecutingAssembly().FullName.Split(new char[] { ',' });
                return new Uri("/" + strArray[0].Trim() + ";component/" + resourceExists, UriKind.RelativeOrAbsolute);
            }
            catch
            {
            }
            return value;
        }

        public object InternalConvertBack(object value, Type targetType, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

