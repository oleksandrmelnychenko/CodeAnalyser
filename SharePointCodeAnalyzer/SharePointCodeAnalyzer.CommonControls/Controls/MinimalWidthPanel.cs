using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class MinimalWidthPanel : Panel
    {
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size size = base.ArrangeOverride(arrangeBounds);
            foreach (UIElement element in base.Children)
            {
                element.Arrange(new Rect(0.0, 0.0, arrangeBounds.Width, arrangeBounds.Height));
            }
            return size;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size availableSize = new Size(base.MinWidth, constraint.Height);
            return base.MeasureOverride(availableSize);
        }
    }
}
