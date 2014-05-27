using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class Row : DependencyObject
    {
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Row), new PropertyMetadata(null));
        public static readonly DependencyProperty RowHeightProperty = DependencyProperty.Register("RowHeight", typeof(double), typeof(Row), new PropertyMetadata(20.0));

        public Row()
        {
            this.Cells = new List<Cell>();
        }

        public Brush Background
        {
            get
            {
                return (Brush)base.GetValue(BackgroundProperty);
            }
            set
            {
                base.SetValue(BackgroundProperty, value);
            }
        }

        public List<Cell> Cells { get; set; }

        public object ReferencedItem { get; set; }

        public double RowHeight
        {
            get
            {
                return (double)base.GetValue(RowHeightProperty);
            }
            set
            {
                base.SetValue(RowHeightProperty, value);
            }
        }
    }
}
