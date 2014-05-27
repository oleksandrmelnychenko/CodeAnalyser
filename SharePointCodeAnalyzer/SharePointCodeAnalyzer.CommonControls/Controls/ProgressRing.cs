using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates"), TemplateVisualState(Name = "Active", GroupName = "ActiveStates"), TemplateVisualState(Name = "Small", GroupName = "SizeStates"), TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    public class ProgressRing : Control
    {
        public static readonly DependencyProperty BindableWidthProperty = DependencyProperty.Register("BindableWidth", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0, new PropertyChangedCallback(ProgressRing.BindableWidthCallback)));
        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0));
        public static readonly DependencyProperty EllipseOffsetProperty = DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(ProgressRing), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressRing), new PropertyMetadata(false, new PropertyChangedCallback(ProgressRing.IsActiveChanged)));
        public static readonly DependencyProperty IsLargeProperty = DependencyProperty.Register("IsLarge", typeof(bool), typeof(ProgressRing), new PropertyMetadata(true, new PropertyChangedCallback(ProgressRing.IsLargeChangedCallback)));
        public static readonly DependencyProperty MaxSideLengthProperty = DependencyProperty.Register("MaxSideLength", typeof(double), typeof(ProgressRing), new PropertyMetadata(0.0));

        static ProgressRing()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
        }

        public ProgressRing()
        {
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        private static void BindableWidthCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is ProgressRing)
            {
                ProgressRing ring = (ProgressRing)dependencyObject;
                ring.SetEllipseDiameter((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset((double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength((double)dependencyPropertyChangedEventArgs.NewValue);
            }
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ProgressRing control = dependencyObject as ProgressRing;
            if (control != null)
            {
                if (control.Name == "ProgressConnectionManager")
                {
                    bool newValue = (bool)dependencyPropertyChangedEventArgs.NewValue;
                }
                if ((bool)dependencyPropertyChangedEventArgs.NewValue)
                {
                    VisualStateManager.GoToState(control, "Active", true);
                }
                else
                {
                    VisualStateManager.GoToState(control, "Inactive", true);
                }
            }
        }

        private static void IsLargeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ProgressRing control = dependencyObject as ProgressRing;
            if (control != null)
            {
                if ((bool)dependencyPropertyChangedEventArgs.NewValue)
                {
                    VisualStateManager.GoToState(control, "Large", true);
                }
                else
                {
                    VisualStateManager.GoToState(control, "Small", true);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            this.UpdateStates();
            base.OnApplyTemplate();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            this.BindableWidth = base.ActualWidth;
        }

        private void SetEllipseDiameter(double width)
        {
            if (width <= 30.0)
            {
                this.EllipseDiameter = 3.0;
            }
            else
            {
                this.EllipseDiameter = (width * 0.1) + 3.0;
            }
        }

        private void SetEllipseOffset(double width)
        {
            if (width <= 30.0)
            {
                this.EllipseOffset = new Thickness(0.0, 12.0, 0.0, 0.0);
            }
            else
            {
                this.EllipseOffset = new Thickness(0.0, (width * 0.4) + 24.0, 0.0, 0.0);
            }
        }

        private void SetMaxSideLength(double width)
        {
            if (width <= 40.0)
            {
                this.MaxSideLength = 40.0;
            }
            else
            {
                this.MaxSideLength = width;
            }
        }

        private void UpdateStates()
        {
            this.IsLarge = !this.IsLarge;
            this.IsLarge = !this.IsLarge;
            this.IsActive = !this.IsActive;
            this.IsActive = !this.IsActive;
        }

        public double BindableWidth
        {
            get
            {
                return (double)base.GetValue(BindableWidthProperty);
            }
            private set
            {
                base.SetValue(BindableWidthProperty, value);
            }
        }

        public double EllipseDiameter
        {
            get
            {
                return (double)base.GetValue(EllipseDiameterProperty);
            }
            private set
            {
                base.SetValue(EllipseDiameterProperty, value);
            }
        }

        public Thickness EllipseOffset
        {
            get
            {
                return (Thickness)base.GetValue(EllipseOffsetProperty);
            }
            private set
            {
                base.SetValue(EllipseOffsetProperty, value);
            }
        }

        public bool IsActive
        {
            get
            {
                return (bool)base.GetValue(IsActiveProperty);
            }
            set
            {
                base.SetValue(IsActiveProperty, value);
            }
        }

        public bool IsLarge
        {
            get
            {
                return (bool)base.GetValue(IsLargeProperty);
            }
            set
            {
                base.SetValue(IsLargeProperty, value);
            }
        }

        public double MaxSideLength
        {
            get
            {
                return (double)base.GetValue(MaxSideLengthProperty);
            }
            private set
            {
                base.SetValue(MaxSideLengthProperty, value);
            }
        }
    }
}
