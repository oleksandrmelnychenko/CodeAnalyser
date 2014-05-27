using System;
using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class GridBasedWrapPanel : Grid
    {
        private Size ArrangeOverride_LastAvailableSize = new Size(0.0, 0.0);
        private Size ArrangeOverride_LastReturnedSize = new Size(0.0, 0.0);
        private int cols = 3;
        private Size LargestItemSize = new Size(0.0, 0.0);
        private int[,] matrix;
        public static readonly DependencyProperty MaxColumnsProperty = DependencyProperty.Register("MaxColumns", typeof(int), typeof(GridBasedWrapPanel), new PropertyMetadata(0x2710, null));
        private Size MeasureOverride_LastAvailableSize = new Size(0.0, 0.0);
        private Size MeasureOverride_LastReturnedSize = new Size(0.0, 0.0);
        public static readonly DependencyProperty MinColumnsProperty = DependencyProperty.Register("MinColumns", typeof(int), typeof(GridBasedWrapPanel), new PropertyMetadata(3, null));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(GridBasedWrapPanel), new PropertyMetadata(System.Windows.Controls.Orientation.Horizontal, null));
        private int rows = 2;
        private Size SmallestItemSize = new Size(0.0, 0.0);

        public GridBasedWrapPanel()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.GridBasedWrapPanel_SizeChanged);
        }

        private bool AddChildToMatrix(int childIndex, int neededColumns, int neededRows)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if ((this.matrix[j, i] < 0) && this.ElementFitsIntoMatrix(j, i, neededColumns, neededRows))
                    {
                        this.PutElementIntoMatrix(childIndex, j, i, neededColumns, neededRows);
                        return true;
                    }
                }
            }
            return false;
        }

        protected override Size ArrangeOverride(Size availableSize)
        {
            Size arrangeSize = availableSize;
            if (base.Children.Count == 0)
            {
                return base.ArrangeOverride(arrangeSize);
            }
            if (arrangeSize.Height <= 10.0)
            {
                arrangeSize.Height = 300.0;
            }
            if (arrangeSize.Width <= 10.0)
            {
                arrangeSize.Width = 300.0;
            }
            if (this.ChildsAreAllOfSameSize())
            {
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    this.UsedSize = this.SimpleArrange(double.MaxValue, arrangeSize.Height);
                    arrangeSize.Width = this.UsedSize.Width;
                }
                else
                {
                    this.UsedSize = this.SimpleArrange(arrangeSize.Width, double.MaxValue);
                    arrangeSize.Height = this.UsedSize.Height;
                }
            }
            else if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                this.UsedSize = this.CalculateMatrix(double.MaxValue, arrangeSize.Height);
                arrangeSize.Width = this.UsedSize.Width;
            }
            else
            {
                this.UsedSize = this.CalculateMatrix(arrangeSize.Width, double.MaxValue);
                arrangeSize.Height = this.UsedSize.Height;
            }
            this.ArrangeOverride_LastAvailableSize = availableSize;
            this.ArrangeOverride_LastReturnedSize = arrangeSize;
            return arrangeSize;
        }

        private Size CalculateMatrix(double availableWidth, double availableHeight)
        {
            this.cols = this.MinColumns;
            this.rows = 2;
            while (!this.TryToArrangeChildsInMatrix(availableWidth, availableHeight))
            {
                if (this.rows <= 0x3e8)
                {
                    int cols = this.cols;
                }
                if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
                {
                    if ((this.GetCurrentSize().Height + this.GetSmallestItemSize().Height) < availableHeight)
                    {
                        this.rows++;
                    }
                    else
                    {
                        this.cols++;
                    }
                }
                else if ((this.GetCurrentSize().Width + this.GetSmallestItemSize().Width) < availableWidth)
                {
                    this.cols++;
                }
                else
                {
                    this.rows++;
                }
            }
            string str = "";
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    str = str + this.matrix[j, i].ToString();
                }
                str = str + Environment.NewLine;
            }
            int childIndex = 0;
            foreach (UIElement element in base.Children)
            {
                if (this.IsChildVisible(element))
                {
                    Point pointForChild = this.GetPointForChild(childIndex);
                    element.Arrange(new Rect(pointForChild, new Point(pointForChild.X + element.DesiredSize.Width, pointForChild.Y + element.DesiredSize.Height)));
                    childIndex++;
                }
            }
            return this.GetCurrentSize();
        }

        private bool ChildsAreAllOfSameSize()
        {
            return false;
        }

        private void CreateNewMatrix(int cols, int rows)
        {
            this.matrix = new int[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    this.matrix[j, i] = -1;
                }
            }
        }

        private bool ElementFitsIntoMatrix(int startColumn, int startRow, int neededColumns, int neededRows)
        {
            if ((startColumn + (neededColumns - 1)) >= this.cols)
            {
                return false;
            }
            if ((startRow + (neededRows - 1)) >= this.rows)
            {
                return false;
            }
            for (int i = startRow; i < (startRow + neededRows); i++)
            {
                for (int j = startColumn; j < (startColumn + neededColumns); j++)
                {
                    if (this.matrix[j, i] >= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Size GetCurrentSize()
        {
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if (this.matrix[j, i] != -1)
                    {
                        if (j > num)
                        {
                            num = j;
                        }
                        if (i > num2)
                        {
                            num2 = i;
                        }
                    }
                }
            }
            return new Size((num + 1) * this.GetSmallestItemSize().Width, (num2 + 1) * this.GetSmallestItemSize().Height);
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

        private Point GetPointForChild(int childIndex)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    if (this.matrix[j, i] == childIndex)
                    {
                        return new Point(j * this.GetSmallestItemSize().Width, i * this.GetSmallestItemSize().Height);
                    }
                }
            }
            return new Point(0.0, 0.0);
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
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            if (base.Children.Count > 0)
            {
                Size size = new Size(this.MinColumns * this.GetSmallestItemSize().Width, this.rows * this.GetSmallestItemSize().Height);
                this.MeasureOverride_LastAvailableSize = availableSize;
                this.MeasureOverride_LastReturnedSize = size;
                return size;
            }
            return base.MeasureOverride(availableSize);
        }

        private void PutElementIntoMatrix(int childIndex, int startColumn, int startRow, int neededColumns, int neededRows)
        {
            for (int i = startRow; i < (startRow + neededRows); i++)
            {
                for (int j = startColumn; j < (startColumn + neededColumns); j++)
                {
                    this.matrix[j, i] = childIndex;
                }
            }
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
                        if (num2 >= this.MaxColumns)
                        {
                            element.Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
                        }
                        else
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
                        }
                    }
                    num++;
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
                    height = point.X + element2.DesiredSize.Height;
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
                }
                num++;
            }
            return new Size(availableWidth, height);
        }

        private bool TryToArrangeChildsInMatrix(double availableWidth, double availableHeight)
        {
            this.CreateNewMatrix(this.cols, this.rows);
            int childIndex = 0;
            foreach (UIElement element in base.Children)
            {
                if (this.IsChildVisible(element))
                {
                    int neededColumns = (int)Math.Ceiling((double)(element.DesiredSize.Width / this.GetSmallestItemSize().Width));
                    int neededRows = (int)Math.Ceiling((double)(element.DesiredSize.Height / this.GetSmallestItemSize().Height));
                    if (neededColumns > this.cols)
                    {
                        this.cols = neededColumns;
                        return this.TryToArrangeChildsInMatrix(availableWidth, availableHeight);
                    }
                    if (neededRows > this.rows)
                    {
                        this.rows = neededRows;
                        return this.TryToArrangeChildsInMatrix(availableWidth, availableHeight);
                    }
                    if (!this.AddChildToMatrix(childIndex, neededColumns, neededRows))
                    {
                        return false;
                    }
                    childIndex++;
                }
            }
            if ((this.rows <= 100) && (this.cols <= 100))
            {
                Size currentSize = this.GetCurrentSize();
                if (currentSize.Height > availableHeight)
                {
                    return false;
                }
                if (currentSize.Width > availableWidth)
                {
                    return false;
                }
            }
            return true;
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
