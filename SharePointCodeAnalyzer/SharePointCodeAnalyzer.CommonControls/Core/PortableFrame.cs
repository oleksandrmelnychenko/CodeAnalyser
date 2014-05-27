using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class PortableFrame : Frame
    {
        public static readonly DependencyProperty SnappedStateProperty = DependencyProperty.Register("SnappedState", typeof(string), typeof(PortableFrame), new PropertyMetadata(null, null));

        static PortableFrame()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PortableFrame), new FrameworkPropertyMetadata(typeof(PortableFrame)));
        }

        public void InternalOnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
        }

        public string SnappedState
        {
            get
            {
                return (string)base.GetValue(SnappedStateProperty);
            }
            set
            {
                base.SetValue(SnappedStateProperty, value);
            }
        }
    }
}
