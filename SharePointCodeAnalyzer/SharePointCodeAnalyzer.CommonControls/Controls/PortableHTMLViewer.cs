using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class PortableHTMLViewer : ContentControl
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.Register("Html", typeof(string), typeof(PortableHTMLViewer), new PropertyMetadata(null, new PropertyChangedCallback(PortableHTMLViewer.OnHtmlChanged)));
        private WebBrowser webViewer;

        public PortableHTMLViewer()
        {
            WebBrowser browser = new WebBrowser
            {
                Visibility = Visibility.Collapsed
            };
            this.webViewer = browser;
            base.Content = this.webViewer;
            this.webViewer.LoadCompleted += new LoadCompletedEventHandler(this.webViewer_LoadCompleted);
        }

        private void InternalOnApplyTemplate()
        {
            base.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            base.VerticalContentAlignment = VerticalAlignment.Stretch;
            base.HorizontalAlignment = HorizontalAlignment.Stretch;
            base.VerticalAlignment = VerticalAlignment.Stretch;
            this.webViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.webViewer.VerticalAlignment = VerticalAlignment.Stretch;
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
            base.OnApplyTemplate();
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PortableHTMLViewer).SetHtml(e.NewValue.ToString());
        }

        private void SetHtml(string htmlCode)
        {
            this.webViewer.NavigateToString(htmlCode);
        }

        private void webViewer_LoadCompleted(object sender, NavigationEventArgs e)
        {
            this.webViewer.Visibility = Visibility.Visible;
        }

        public string Html
        {
            get
            {
                return (string)base.GetValue(HtmlProperty);
            }
            set
            {
                base.SetValue(HtmlProperty, value);
            }
        }
    }
}
