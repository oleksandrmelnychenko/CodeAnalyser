using System;
using System.Windows;
using System.Windows.Controls;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class UniformGridPanel : Grid, ISupportsVirtualization
    {
        private int cachedCalculateMaxVisibleItems = 0x2710;
        private System.Windows.Controls.ItemContainerGenerator itemContainerGenerator;
        private Size LargestItemSize = new Size(0.0, 0.0);
        public static readonly DependencyProperty MaxColumnsProperty = DependencyProperty.Register("MaxColumns", typeof(int), typeof(UniformGridPanel), new PropertyMetadata(0x2710, null));
        private int maxItemsGenerated;
        public static readonly DependencyProperty MinColumnsProperty = DependencyProperty.Register("MinColumns", typeof(int), typeof(UniformGridPanel), new PropertyMetadata(3, null));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(UniformGridPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, null));
        private Size SmallestItemSize = new Size(0.0, 0.0);

        public event Action<ISupportsVirtualization, bool> OnNotAllItemsShowChanged;

        public UniformGridPanel()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.GridBasedWrapPanel_SizeChanged);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            try
            {
                if (base.Children.Count == 0)
                {
                    return new Size(0.0, 0.0);
                }
                UIElement element = base.Children[0];
                Size itemSize = new Size(element.DesiredSize.Width, element.DesiredSize.Height);
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
                    this.UsedSize = this.SimpleArrange(itemSize, double.MaxValue, finalSize.Height, true);
                    finalSize.Width = this.UsedSize.Width;
                    return finalSize;
                }
                this.UsedSize = this.SimpleArrange(itemSize, finalSize.Width, double.MaxValue, true);
                finalSize.Height = this.UsedSize.Height;
            }
            catch
            {
            }
            return finalSize;
        }

        public int CalculateMaxVisibleItems(Size assumedItemSize)
        {
            if (this.MaxColumns != 0x2710)
            {
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    double num = 1200.0;
                    int num2 = (int)Math.Round((double)(num / assumedItemSize.Height));
                    this.cachedCalculateMaxVisibleItems = this.MaxColumns * num2;
                }
                else
                {
                    this.cachedCalculateMaxVisibleItems = 10;
                }
            }
            return this.cachedCalculateMaxVisibleItems;
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
                if (base.Children.Count == 0)
                {
                    return new Size(0.0, 0.0);
                }
                Size itemSize = new Size(0.0, 0.0);
                for (int i = 0; i < base.Children.Count; i++)
                {
                    if (i <= this.cachedCalculateMaxVisibleItems)
                    {
                        base.Children[i].Measure(new Size(10000.0, 10000.0));
                        if (i == 0)
                        {
                            itemSize = new Size(base.Children[i].DesiredSize.Width, base.Children[i].DesiredSize.Height);
                        }
                    }
                }
                Size size2 = availableSize;
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.UsedSize = this.SimpleArrange(itemSize, double.MaxValue, availableSize.Height, false);
                    size2.Width = this.UsedSize.Width;
                    return size2;
                }
                this.UsedSize = this.SimpleArrange(itemSize, availableSize.Width, double.MaxValue, false);
                size2.Height = this.UsedSize.Height;
                return size2;
            }
            catch
            {
            }
            return new Size(0.0, 0.0);
        }

        private Size SimpleArrange(Size itemSize, double availableWidth, double availableHeight, bool withChildArrange)
        {
            Point point = new Point(0.0, 0.0);
            int num = 0;
            int num2 = 0;
            bool flag = false;
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                double width = 0.0;
                int num4 = 0;
                foreach (UIElement element in base.Children)
                {
                    if (num2 >= this.MaxColumns)
                    {
                        flag = true;
                        if (withChildArrange)
                        {
                            element.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                        }
                    }
                    else if (this.IsChildVisible(element))
                    {
                        num4++;
                        if (withChildArrange)
                        {
                            element.Arrange(new Rect(point, new Point(point.X + element.DesiredSize.Width, point.Y + element.DesiredSize.Height)));
                        }
                        width = point.X + element.DesiredSize.Width;
                        point.Y += element.DesiredSize.Height;
                        if (((num + 1) < base.Children.Count) && ((point.Y + element.DesiredSize.Height) > availableHeight))
                        {
                            point.Y = 0.0;
                            point.X += element.DesiredSize.Width;
                            num2++;
                        }
                        num++;
                    }
                }
                if (this.OnNotAllItemsShowChanged != null)
                {
                    this.OnNotAllItemsShowChanged(this, flag);
                }
                return new Size(width, availableHeight);
            }
            double height = 0.0;
            int num6 = 0;
            foreach (UIElement element2 in base.Children)
            {
                if (this.IsChildVisible(element2))
                {
                    num6++;
                    if (withChildArrange)
                    {
                        element2.Arrange(new Rect(point, new Point(point.X + itemSize.Width, point.Y + itemSize.Height)));
                    }
                    height = point.Y + itemSize.Height;
                    point.X += itemSize.Width;
                    if (((num + 1) < base.Children.Count) && ((point.X + itemSize.Width) > availableWidth))
                    {
                        point.X = 0.0;
                        point.Y += itemSize.Height;
                    }
                    num++;
                }
            }
            return new Size(availableWidth, height);
        }

        public System.Windows.Controls.ItemContainerGenerator ItemContainerGenerator
        {
            get
            {
                return this.itemContainerGenerator;
            }
            set
            {
                this.itemContainerGenerator = value;
            }
        }

        public int MaxColumns
        {
            get
            {
                return (int)base.GetValue(MaxColumnsProperty);
            }
            set
            {
                base.SetValue(MaxColumnsProperty, value);
            }
        }

        public int MinColumns
        {
            get
            {
                return (int)base.GetValue(MinColumnsProperty);
            }
            set
            {
                base.SetValue(MinColumnsProperty, value);
            }
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
