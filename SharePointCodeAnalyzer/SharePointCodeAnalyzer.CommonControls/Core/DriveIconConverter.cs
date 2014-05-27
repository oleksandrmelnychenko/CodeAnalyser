using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class DriveIconConverter : IValueConverter
    {
        private static BitmapImage cdrom;
        private static BitmapImage drive;
        private static BitmapImage folder;
        private static BitmapImage netDrive;
        private static BitmapImage ram;
        private static BitmapImage removable;

        public DriveIconConverter()
        {
            if (removable == null)
            {
                removable = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_8.ico");
            }
            if (drive == null)
            {
                drive = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_9.ico");
            }
            if (netDrive == null)
            {
                netDrive = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_10.ico");
            }
            if (cdrom == null)
            {
                cdrom = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_12.ico");
            }
            if (ram == null)
            {
                ram = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_303.ico");
            }
            if (folder == null)
            {
                folder = this.CreateImage("pack://application:,,,/SPCAF.Client.WPF;component/NonShared/Images/shell32_264.ico");
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TreeItem item = value as TreeItem;
            //if (item == null)
            //{
            //    throw new ArgumentException("Illegal item type");
            //}
            //if (!(item is DriveTreeItem))
            //{
            //    return folder;
            //}
            //DriveTreeItem item2 = item as DriveTreeItem;
            //switch (item2.DriveType)
            //{
            //    case DriveType.Unknown:
            //        return drive;

            //    case DriveType.NoRootDirectory:
            //        return drive;

            //    case DriveType.Removable:
            //        return removable;

            //    case DriveType.Fixed:
            //        return drive;

            //    case DriveType.Network:
            //        return netDrive;

            //    case DriveType.CDRom:
            //        return cdrom;

            //    case DriveType.Ram:
            //        return ram;
            //}
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private BitmapImage CreateImage(string uri)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(uri);
            image.EndInit();
            return image;
        }
    }
}

