using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class HyperLinkButton : Button
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(HyperLinkButton), new PropertyMetadata(null));
        public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url", typeof(string), typeof(HyperLinkButton), new PropertyMetadata(null));

        static HyperLinkButton()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(HyperLinkButton), new FrameworkPropertyMetadata(typeof(HyperLinkButton)));
        }

        public HyperLinkButton()
        {
            base.Click += new RoutedEventHandler(this.HyperLinkButton_Click);
        }

        private void HyperLinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(this.Url);
            }
            catch (Exception)
            {
            }
        }

        public string Url
        {
            get
            {
                return (string)base.GetValue(UrlProperty);
            }
            set
            {
                base.SetValue(UrlProperty, value);
            }
        }
    }
}
