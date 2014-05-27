using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NormalButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                ChangeNormalButtonBackgroundImage("/Images/assets/windowmaximize.png");
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                ChangeNormalButtonBackgroundImage("/Images/assets/windowNormal.png");
            }
        }

        private void ChangeNormalButtonBackgroundImage(string url)
        {
            var image = NormalButton.Content as Image;
            if (image != null)
            {
                image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }
        }
    }
}
