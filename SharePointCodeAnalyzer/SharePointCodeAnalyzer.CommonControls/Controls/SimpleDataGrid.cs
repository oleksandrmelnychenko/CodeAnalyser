using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class SimpleDataGrid : ItemsControl
    {
        public static readonly DependencyProperty AlternateRowColorProperty = DependencyProperty.Register("AlternateRowColor", typeof(Brush), typeof(SimpleDataGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(SimpleDataGrid), new PropertyMetadata(null));
        private List<double> columnsWidths = new List<double>();
        private bool columnwidthCalculated;
        public static readonly DependencyProperty DataMemberProperty = DependencyProperty.Register("DataMember", typeof(string), typeof(SimpleDataGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(SimpleDataGrid), new PropertyMetadata(null));
        private ItemsControl headerItemsControl;
        public static readonly DependencyProperty HeaderMemberProperty = DependencyProperty.Register("HeaderMember", typeof(string), typeof(SimpleDataGrid), new PropertyMetadata(null));
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(SimpleDataGrid), new PropertyMetadata(null));
        private Size lastSize = new Size(0.0, 0.0);
        public static readonly DependencyProperty RowColorProperty = DependencyProperty.Register("RowColor", typeof(Brush), typeof(SimpleDataGrid), new PropertyMetadata(null));

        static SimpleDataGrid()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleDataGrid), new FrameworkPropertyMetadata(typeof(SimpleDataGrid)));
        }

        public SimpleDataGrid()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.SimpleGrid_SizeChanged);
            this.Headers = new ObservableCollection<Header>();
            this.Rows = new ObservableCollection<Row>();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (this.lastSize.Width != arrangeBounds.Width)
            {
                double num = arrangeBounds.Width - 25.0;
                Dictionary<int, double> dictionary = new Dictionary<int, double>();
                if (this.Headers.Sum<Header>(((Func<Header, double>)(header => header.ColumnMinWidth))) <= num)
                {
                    int key = 0;
                    foreach (Header header in this.Headers)
                    {
                        double num4 = header.ColumnMinWidth / this.Headers.Sum<Header>(((Func<Header, double>)(h => h.ColumnMinWidth)));
                        double num5 = num * num4;
                        dictionary.Add(key, num5);
                        key++;
                    }
                }
                else
                {
                    int num6 = 0;
                    foreach (Header header2 in this.Headers)
                    {
                        if (!header2.HasMultilineContent)
                        {
                            dictionary.Add(num6, header2.ColumnMinWidth);
                        }
                        else
                        {
                            double num7 = num - (from h in this.Headers
                                                 where !h.HasMultilineContent
                                                 select h).Sum<Header>(((Func<Header, double>)(h => h.ColumnMinWidth)));
                            double num8 = header2.ColumnMinWidth / (from h in this.Headers
                                                                    where h.HasMultilineContent
                                                                    select h).Sum<Header>(((Func<Header, double>)(h => h.ColumnMinWidth)));
                            double num9 = num7 * num8;
                            if (num9 < 100.0)
                            {
                                num9 = 100.0;
                            }
                            dictionary.Add(num6, num9);
                        }
                        num6++;
                    }
                }
                int num10 = 0;
                foreach (Header header3 in this.Headers)
                {
                    header3.Width = dictionary[num10];
                    num10++;
                }
                foreach (Row row in this.Rows)
                {
                    num10 = 0;
                    foreach (Cell cell in row.Cells)
                    {
                        cell.Width = dictionary[num10];
                        num10++;
                    }
                }
                this.lastSize = arrangeBounds;
            }
            return base.ArrangeOverride(arrangeBounds);
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            try
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if ((child != null) && (child is childItem))
                    {
                        return (childItem)child;
                    }
                    childItem local = this.FindVisualChild<childItem>(child);
                    if (local != null)
                    {
                        return local;
                    }
                }
            }
            catch
            {
            }
            return default(childItem);
        }

        private double GetMinWidthOfColumn(Row row, int col)
        {
            if (row.Cells.Count > col)
            {
                return row.Cells[col].MinWidth;
            }
            return 0.0;
        }

        private string GetStringValue(object headerColumn)
        {
            if (headerColumn != null)
            {
                return headerColumn.ToString();
            }
            return "";
        }

        protected override Size MeasureOverride(Size arrangeBounds)
        {
            return base.MeasureOverride(arrangeBounds);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.headerItemsControl = base.GetTemplateChild("PART_Headers") as ItemsControl;
        }

        private static void OnHeaderSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SimpleDataGrid).ResetGrid();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            this.ResetGrid();
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            this.ResetGrid();
        }

        private double RecalcPercentages(double availwidth)
        {
            double num = ((IEnumerable<double>)this.columnsWidths).Sum();
            if ((num > availwidth) || double.IsInfinity(availwidth))
            {
                availwidth = num;
                if ((base.MinWidth > 0.0) && (base.MinWidth > num))
                {
                    availwidth = base.MinWidth;
                }
            }
            double num2 = availwidth / num;
            for (int i = 0; i < this.columnsWidths.Count; i++)
            {
                this.columnsWidths[i] *= num2;
            }
            return availwidth;
        }

        private void ResetGrid()
        {
            try
            {
                if ((this.HeaderMember == null) || (this.DataMember == null))
                {
                    return;
                }
                this.Headers.Clear();
                this.Rows.Clear();
                this.columnwidthCalculated = false;
                new List<double>();
                if ((base.Items != null) && (base.Items.Count > 0))
                {
                    object notification = base.Items[0];
                    object obj3 = this.TryGetProperty(notification, this.HeaderMember);
                    if ((obj3 != null) && (obj3 is IEnumerable))
                    {
                        foreach (object obj4 in obj3 as IEnumerable)
                        {
                            Header item = new Header
                            {
                                Content = this.GetStringValue(obj4),
                                ContentTemplate = this.HeaderTemplate
                            };
                            this.Headers.Add(item);
                        }
                    }
                    int num = 0;
                    int num2 = 0;
                    foreach (object obj5 in (IEnumerable)base.Items)
                    {
                        Brush rowColor = this.RowColor;
                        if ((num % 2) == 0)
                        {
                            rowColor = this.AlternateRowColor;
                        }
                        Row row = new Row
                        {
                            Background = rowColor,
                            ReferencedItem = obj5
                        };
                        object obj6 = this.TryGetProperty(obj5, this.DataMember);
                        if ((obj6 != null) && (obj6 is IEnumerable))
                        {
                            int num3 = 0;
                            foreach (object obj7 in obj6 as IEnumerable)
                            {
                                Cell cell = new Cell
                                {
                                    Content = this.GetStringValue(obj7),
                                    ContentTemplate = this.CellTemplate
                                };
                                row.Cells.Add(cell);
                                num3++;
                            }
                            if (num2 < num3)
                            {
                                num2 = num3;
                            }
                        }
                        this.Rows.Add(row);
                        num++;
                    }
                }
            }
            catch (Exception)
            {
            }
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
            foreach (Row row3 in this.Rows)
            {
                double height = 0.0;
                int key = 0;
                foreach (Cell cell2 in row3.Cells)
                {
                    cell2.Measure(new Size(10000.0, 10000.0));
                    cell2.SimpleGridMinWidth = cell2.DesiredSize.Width;
                    if (height < cell2.DesiredSize.Height)
                    {
                        height = cell2.DesiredSize.Height;
                    }
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, cell2.SimpleGridMinWidth);
                    }
                    else if (dictionary[key] < cell2.SimpleGridMinWidth)
                    {
                        dictionary[key] = cell2.SimpleGridMinWidth;
                    }
                    if (!dictionary2.ContainsKey(key))
                    {
                        dictionary2.Add(key, cell2.CellHasMultilineContent);
                    }
                    else if (!dictionary2[key] && cell2.CellHasMultilineContent)
                    {
                        dictionary2[key] = cell2.CellHasMultilineContent;
                    }
                    key++;
                }
                row3.RowHeight = height;
            }
            int num6 = 0;
            foreach (Header header2 in this.Headers)
            {
                header2.ColumnMinWidth = dictionary[num6];
                header2.HasMultilineContent = dictionary2[num6];
                num6++;
            }
        }

        private void SetExpectedColumnWidth(double expectedWidth, int colCount)
        {
            if (colCount >= this.columnsWidths.Count)
            {
                this.columnsWidths.Add(expectedWidth);
            }
            else if (expectedWidth > this.columnsWidths[colCount])
            {
                this.columnsWidths[colCount] = expectedWidth;
            }
        }

        private void SimpleGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.columnsWidths.Clear();
        }

        private object TryGetProperty(object notification, string GroupByProperty)
        {
            PropertyInfo property = null;
            property = notification.GetType().GetProperty(GroupByProperty);
            if (property != null)
            {
                object obj2 = property.GetValue(notification, null);
                if (obj2 != null)
                {
                    return obj2;
                }
            }
            return null;
        }

        public Brush AlternateRowColor
        {
            get
            {
                return (Brush)base.GetValue(AlternateRowColorProperty);
            }
            set
            {
                base.SetValue(AlternateRowColorProperty, value);
            }
        }

        public DataTemplate CellTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(CellTemplateProperty);
            }
            set
            {
                base.SetValue(CellTemplateProperty, value);
            }
        }

        public string DataMember
        {
            get
            {
                return (string)base.GetValue(DataMemberProperty);
            }
            set
            {
                base.SetValue(DataMemberProperty, value);
            }
        }

        public Brush HeaderBackground
        {
            get
            {
                return (Brush)base.GetValue(HeaderBackgroundProperty);
            }
            set
            {
                base.SetValue(HeaderBackgroundProperty, value);
            }
        }

        public string HeaderMember
        {
            get
            {
                return (string)base.GetValue(HeaderMemberProperty);
            }
            set
            {
                base.SetValue(HeaderMemberProperty, value);
            }
        }

        public ObservableCollection<Header> Headers { get; private set; }

        public DataTemplate HeaderTemplate
        {
            get
            {
                return (DataTemplate)base.GetValue(HeaderTemplateProperty);
            }
            set
            {
                base.SetValue(HeaderTemplateProperty, value);
            }
        }

        public Brush RowColor
        {
            get
            {
                return (Brush)base.GetValue(RowColorProperty);
            }
            set
            {
                base.SetValue(RowColorProperty, value);
            }
        }

        public ObservableCollection<Row> Rows { get; private set; }
    }
}
