using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class SimpleWrapPanel : Grid
    {
        private Size LargestItemSize = new Size(0.0, 0.0);
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(SimpleWrapPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, null));
        private Size SmallestItemSize = new Size(0.0, 0.0);

        public SimpleWrapPanel()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.GridBasedWrapPanel_SizeChanged);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            try
            {
                if (base.Children.Count == 0)
                {
                    return finalSize;
                }
                if (finalSize.Height <= 10.0)
                {
                    finalSize.Height = 300.0;
                }
                if (finalSize.Width <= 10.0)
                {
                    finalSize.Width = 300.0;
                }
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.UsedSize = this.SimpleArrange(double.MaxValue, finalSize.Height);
                    finalSize.Width = this.UsedSize.Width;
                    return finalSize;
                }
                this.UsedSize = this.SimpleArrange(finalSize.Width, double.MaxValue);
                finalSize.Height = this.UsedSize.Height;
            }
            catch
            {
            }
            return finalSize;
        }

        private Size GetLargestItemSize()
        {
            if (this.LargestItemSize.Width == 0.0)
            {
                double width = 0.0;
                double height = 0.0;
                foreach (UIElement element in base.Children)
                {
                    if (this.IsChildVisible(element))
                    {
                        if (element.DesiredSize.Width > width)
                        {
                            width = element.DesiredSize.Width;
                        }
                        if (element.DesiredSize.Height > height)
                        {
                            height = element.DesiredSize.Height;
                        }
                    }
                }
                this.LargestItemSize = new Size(width, height);
            }
            return this.LargestItemSize;
        }

        private Size GetSmallestItemSize()
        {
            double width = 10000.0;
            double height = 10000.0;
            foreach (UIElement element in base.Children)
            {
                if (this.IsChildVisible(element))
                {
                    if ((element.DesiredSize.Width < width) && (element.DesiredSize.Width > 0.0))
                    {
                        width = element.DesiredSize.Width;
                    }
                    if ((element.DesiredSize.Height < height) && (element.DesiredSize.Height > 0.0))
                    {
                        height = element.DesiredSize.Height;
                    }
                }
            }
            this.SmallestItemSize = new Size(width, height);
            return this.SmallestItemSize;
        }

        private void GridBasedWrapPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                base.Width = e.NewSize.Width;
            }
            if (this.Orientation == System.Windows.Controls.Orientation.Vertical)
            {
                base.Height = e.NewSize.Height;
            }
        }

        private bool IsChildVisible(UIElement child)
        {
            return (child.Visibility == Visibility.Visible);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            try
            {
                foreach (UIElement element in base.Children)
                {
                    if (this.IsChildVisible(element))
                    {
                        element.Measure(new Size(10000.0, 10000.0));
                    }
                }
                Size size = availableSize;
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.UsedSize = this.SimpleArrange(double.MaxValue, availableSize.Height);
                    size.Width = this.UsedSize.Width;
                    return size;
                }
                this.UsedSize = this.SimpleArrange(availableSize.Width, double.MaxValue);
                size.Height = this.UsedSize.Height;
                return size;
            }
            catch
            {
            }
            return new Size(0.0, 0.0);
        }

        private Size SimpleArrange(double availableWidth, double availableHeight)
        {
            Point point = new Point(0.0, 0.0);
            int num = 0;
            int num2 = 0;
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                double num3 = 0.0;
                double width = 0.0;
                foreach (UIElement element in base.Children)
                {
                    if (this.IsChildVisible(element))
                    {
                        element.Arrange(new Rect(point, new Point(point.X + element.DesiredSize.Width, point.Y + element.DesiredSize.Height)));
                        width = point.X + element.DesiredSize.Width;
                        if (element.DesiredSize.Width > num3)
                        {
                            num3 = element.DesiredSize.Width;
                        }
                        point.Y += element.DesiredSize.Height;
                        if (((num + 1) < base.Children.Count) && ((point.Y + base.Children[num + 1].DesiredSize.Height) > availableHeight))
                        {
                            point.Y = 0.0;
                            point.X += num3;
                            num3 = 0.0;
                            num2++;
                        }
                        num++;
                    }
                }
                return new Size(width, availableHeight);
            }
            double num5 = 0.0;
            double height = 0.0;
            foreach (UIElement element2 in base.Children)
            {
                if (this.IsChildVisible(element2))
                {
                    element2.Arrange(new Rect(point, new Point(point.X + element2.DesiredSize.Width, point.Y + element2.DesiredSize.Height)));
                    height = point.Y + element2.DesiredSize.Height;
                    if (element2.DesiredSize.Height > num5)
                    {
                        num5 = element2.DesiredSize.Height;
                    }
                    point.X += element2.DesiredSize.Width;
                    if (((num + 1) < base.Children.Count) && ((point.X + base.Children[num + 1].DesiredSize.Width) > availableWidth))
                    {
                        point.X = 0.0;
                        point.Y += num5;
                        num5 = 0.0;
                    }
                    num++;
                }
            }
            return new Size(availableWidth, height);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get
            {
                return (System.Windows.Controls.Orientation)base.GetValue(OrientationProperty);
            }
            set
            {
                base.SetValue(OrientationProperty, value);
            }
        }

        public Size UsedSize { get; set; }
    }
}
