using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class Header : ContentControl
    {
        public Header()
        {
            base.HorizontalAlignment = HorizontalAlignment.Stretch;
            base.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        }

        public double ColumnMinWidth { get; set; }

        public bool HasMultilineContent { get; set; }

        public double SimpleGridMinWidth { get; set; }
    }
}
