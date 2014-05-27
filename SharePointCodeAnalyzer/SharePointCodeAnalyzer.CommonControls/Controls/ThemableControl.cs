using System.Windows;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class ThemableControl
    {
        public ThemableControl(FrameworkElement stylableControl)
        {
            this.StylableControl = stylableControl;
            this.StylableControl.Loaded += new RoutedEventHandler(this.stylableControl_Loaded);
        }

        public void ReapplyStyle()
        {
        }

        private void stylableControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitialStyle = this.StylableControl.Style;
            this.InitialStyleKey = ResourceHelper.GetKeyForObject(this.InitialStyle);
        }

        public Style InitialStyle { get; set; }

        public string InitialStyleKey { get; set; }

        public FrameworkElement StylableControl { get; set; }
    }
}
