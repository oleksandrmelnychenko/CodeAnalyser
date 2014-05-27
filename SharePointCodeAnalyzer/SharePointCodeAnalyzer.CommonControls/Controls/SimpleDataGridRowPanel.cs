using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class SimpleDataGridRowPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            try
            {
                if (base.Children.Count == 0)
                {
                    return finalSize;
                }
                double x = 0.0;
                foreach (UIElement element in base.Children)
                {
                    element.Measure(new Size(finalSize.Width, finalSize.Height));
                    element.Arrange(new Rect(x, 0.0, element.DesiredSize.Width, element.DesiredSize.Height));
                    x += element.DesiredSize.Width;
                }
            }
            catch
            {
            }
            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double height = 0.0;
            double width = 0.0;
            try
            {
                foreach (UIElement element in base.Children)
                {
                    element.Measure(new Size(availableSize.Width, availableSize.Height));
                    if (height < element.DesiredSize.Height)
                    {
                        height = element.DesiredSize.Height;
                    }
                    width += element.DesiredSize.Width;
                }
            }
            catch
            {
            }
            return new Size(width, height);
        }
    }
}
