using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class PortableScrollViewer : ContentControl
    {
        private double animationCounter;
        private double animationStartValue;
        private double currentNumber;
        private double distance;
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(PortableScrollViewer), new PropertyMetadata(ScrollBarVisibility.Visible, null));
        private double newNumber;
        private ScrollViewer scrollViewer;
        private DispatcherTimer timer = new DispatcherTimer();
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(PortableScrollViewer), new PropertyMetadata(ScrollBarVisibility.Visible, null));

        static PortableScrollViewer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PortableScrollViewer), new FrameworkPropertyMetadata(typeof(PortableScrollViewer)));
        }

        public PortableScrollViewer()
        {
            this.timer.Tick += new EventHandler(this.timer_Tick);
        }

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            if (parent == null)
            {
                return default(T);
            }
            T local = parent as T;
            if (local != null)
            {
                return local;
            }
            return FindParent<T>(parent);
        }

        private double getFormula(EasingFunction animType, double t, double b, double d, double c)
        {
            switch (animType)
            {
                case EasingFunction.Linear:
                    return (((c * t) / d) + b);

                case EasingFunction.EaseInQuad:
                    return (((c * (t /= d)) * t) + b);

                case EasingFunction.EaseOutQuad:
                    return (((-c * (t /= d)) * (t - 2.0)) + b);

                case EasingFunction.EaseInOutQuad:
                    if ((t /= (d / 2.0)) >= 1.0)
                    {
                        return (((-c / 2.0) * ((--t * (t - 2.0)) - 1.0)) + b);
                    }
                    return ((((c / 2.0) * t) * t) + b);

                case EasingFunction.EaseInCubic:
                    return ((((c * (t /= d)) * t) * t) + b);

                case EasingFunction.EaseOutCubic:
                    return ((c * ((((t = (t / d) - 1.0) * t) * t) + 1.0)) + b);

                case EasingFunction.EaseInOutCubic:
                    if ((t /= (d / 2.0)) >= 1.0)
                    {
                        return (((c / 2.0) * ((((t -= 2.0) * t) * t) + 2.0)) + b);
                    }
                    return (((((c / 2.0) * t) * t) * t) + b);

                case EasingFunction.EaseInQuart:
                    return (((((c * (t /= d)) * t) * t) * t) + b);

                case EasingFunction.EaseOutQuart:
                    return ((-c * (((((t = (t / d) - 1.0) * t) * t) * t) - 1.0)) + b);

                case EasingFunction.EaseInExpo:
                    if (t != 0.0)
                    {
                        return ((c * Math.Pow(2.0, 10.0 * ((t / d) - 1.0))) + b);
                    }
                    return b;

                case EasingFunction.EaseOutExpo:
                    if (t != d)
                    {
                        return ((c * (-Math.Pow(2.0, (-10.0 * t) / d) + 1.0)) + b);
                    }
                    return (b + c);
            }
            return 0.0;
        }

        private PortablePage GetParentPage()
        {
            return FindParent<PortablePage>(this);
        }

        private void GoToPosition(double newPosition)
        {
            if (newPosition < 0.0)
            {
                newPosition = 0.0;
            }
            if (newPosition > this.scrollViewer.Width)
            {
                newPosition = this.scrollViewer.Width;
            }
            this.newNumber = newPosition;
            if (this.newNumber != this.currentNumber)
            {
                this.animationCounter = 0.0;
                this.animationStartValue = this.scrollViewer.HorizontalOffset;
                this.timer.Interval = TimeSpan.FromMilliseconds(33.3);
                this.timer.Start();
                this.Tick();
            }
        }

        private void InternalOnApplyTemplate()
        {
            this.scrollViewer = base.GetTemplateChild("PART_ScrollViewer") as ScrollViewer;
            if (this.scrollViewer != null)
            {
                this.scrollViewer.PreviewMouseWheel += new MouseWheelEventHandler(this.x_PreviewMouseWheel);
            }
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
            base.OnApplyTemplate();
        }

        private void Tick()
        {
            if (this.currentNumber != this.newNumber)
            {
                double animationCounter = this.animationCounter;
                double animationStartValue = this.animationStartValue;
                double c = this.newNumber - this.animationStartValue;
                double d = 30.0;
                this.currentNumber = this.getFormula(EasingFunction.EaseOutExpo, animationCounter, animationStartValue, d, c);
                this.animationCounter++;
            }
            else
            {
                this.currentNumber = this.newNumber;
                this.timer.Stop();
            }
            this.scrollViewer.ScrollToHorizontalOffset(this.currentNumber);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

        private void x_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (this.scrollViewer != null)
            {
                double horizontalOffset = this.scrollViewer.HorizontalOffset;
                if (e.Delta < 0)
                {
                    horizontalOffset += 300.0;
                }
                else
                {
                    horizontalOffset -= 300.0;
                }
                e.Handled = true;
                this.GoToPosition(horizontalOffset);
            }
        }

        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get
            {
                return (ScrollBarVisibility)base.GetValue(HorizontalScrollBarVisibilityProperty);
            }
            set
            {
                base.SetValue(HorizontalScrollBarVisibilityProperty, value);
            }
        }

        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get
            {
                return (ScrollBarVisibility)base.GetValue(VerticalScrollBarVisibilityProperty);
            }
            set
            {
                base.SetValue(VerticalScrollBarVisibilityProperty, value);
            }
        }
    }
}
