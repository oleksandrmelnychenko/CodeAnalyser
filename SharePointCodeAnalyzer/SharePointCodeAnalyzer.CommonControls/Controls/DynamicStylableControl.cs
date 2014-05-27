using System.Windows;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class DynamicStylableControl
    {
        private SnappedState lastAppliedState;

        public DynamicStylableControl(FrameworkElement stylableControl, Style defaultStyle, Style snappedStyle, SnappedState initialState)
        {
            this.StylableControl = stylableControl;
            this.DefaultStyle = defaultStyle;
            this.SnappedStyle = snappedStyle;
            this.UpdateState(initialState);
        }

        internal void UpdateState(SnappedState newState)
        {
            if (this.lastAppliedState != newState)
            {
                if (newState == SnappedState.Snapped)
                {
                    this.StylableControl.Style = this.SnappedStyle;
                }
                else
                {
                    this.StylableControl.Style = this.DefaultStyle;
                }
                this.lastAppliedState = newState;
            }
        }

        public Style DefaultStyle { get; set; }

        public Style SnappedStyle { get; set; }

        public FrameworkElement StylableControl { get; set; }
    }
}
