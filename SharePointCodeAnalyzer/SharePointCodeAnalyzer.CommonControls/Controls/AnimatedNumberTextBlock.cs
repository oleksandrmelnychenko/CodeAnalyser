using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class AnimatedNumberTextBlock : Control
    {
        private int currentNumber;
        private int distance;
        private TextBlock mainTextBlock;
        private int newNumber;
        public static readonly DependencyProperty TextBlockStyleProperty = DependencyProperty.Register("TextBlockStyle", typeof(Style), typeof(AnimatedNumberTextBlock), new PropertyMetadata(null));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(AnimatedNumberTextBlock), new PropertyMetadata(null, new PropertyChangedCallback(AnimatedNumberTextBlock.OnTextChanged)));
        private DispatcherTimer timer = new DispatcherTimer();

        static AnimatedNumberTextBlock()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedNumberTextBlock), new FrameworkPropertyMetadata(typeof(AnimatedNumberTextBlock)));
        }

        public AnimatedNumberTextBlock()
        {
            this.timer.Tick += new EventHandler(this.timer_Tick);
        }

        private void InternalOnApplyTemplate()
        {
            this.mainTextBlock = base.GetTemplateChild("PART_TextBlock") as TextBlock;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.InternalOnApplyTemplate();
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimatedNumberTextBlock).UpdateNumber(e.NewValue);
        }

        private void Tick()
        {
            if (this.mainTextBlock != null)
            {
                if ((Math.Abs((int)(this.newNumber - this.currentNumber)) <= Math.Abs(this.distance)) || (Math.Abs(this.distance) < 1))
                {
                    this.mainTextBlock.Text = this.newNumber.ToString();
                    this.timer.Stop();
                }
                else
                {
                    this.currentNumber += this.distance;
                    this.mainTextBlock.Text = this.currentNumber.ToString();
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

        private void UpdateNumber(object p)
        {
            this.currentNumber = 0;
            if ((this.mainTextBlock != null) && !string.IsNullOrEmpty(this.mainTextBlock.Text))
            {
                this.currentNumber = int.Parse(this.mainTextBlock.Text);
            }
            this.newNumber = int.Parse(p.ToString());
            this.distance = (int)Math.Ceiling((double)(((double)(this.newNumber - this.currentNumber)) / 10.0));
            Math.Abs(this.distance);
            this.timer.Interval = TimeSpan.FromSeconds(0.025);
            this.timer.Start();
        }

        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        public Style TextBlockStyle
        {
            get
            {
                return (Style)base.GetValue(TextBlockStyleProperty);
            }
            set
            {
                base.SetValue(TextBlockStyleProperty, value);
            }
        }
    }
}
