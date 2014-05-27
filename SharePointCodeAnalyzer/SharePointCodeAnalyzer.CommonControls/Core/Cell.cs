using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class Cell : ContentControl
    {
        public bool CellHasMultilineContent
        {
            get
            {
                return ((base.Content is string) && base.Content.ToString().Contains(" "));
            }
        }

        public double SimpleGridMinWidth { get; set; }
    }
}
