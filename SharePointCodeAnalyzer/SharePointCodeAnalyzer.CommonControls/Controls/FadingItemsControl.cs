using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class FadingItemsControl : ItemsControl
    {
        // Fields
        public static readonly DependencyProperty IsNotAllItemsShownProperty = 
            DependencyProperty.Register("IsNotAllItemsShown", typeof(bool), typeof(FadingItemsControl), new PropertyMetadata(false));
        private Panel itemsHost;
        private int itemsHostHasLimitedNumberOfItemsToDisplay;

        // Methods
        //private void FadingItemsControl_OnNotAllItemsShowChanged(ISupportsVirtualization arg1, bool notAllItemsShown)
        //{
        //    this.IsNotAllItemsShown = notAllItemsShown;
        //}

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return ((item is FrameworkElement) || base.IsItemItsOwnContainerOverride(item));
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (base.Items != null)
            {
                int num = base.ItemContainerGenerator.IndexFromContainer(element);
                if (base.Items.Count < 100)
                {
                    ContentPresenter presenter = (ContentPresenter)element;
                    TimeSpan span = TimeSpan.FromMilliseconds(num * (500.0 / ((double)base.Items.Count)));
                    presenter.Opacity = 0.0;
                    DoubleAnimation animation = new DoubleAnimation
                    {
                        From = 0.0,
                        To = 1.0,
                        Duration = TimeSpan.FromMilliseconds(250.0),
                        BeginTime = new TimeSpan?(span)
                    };
                    Storyboard storyboard = new Storyboard
                    {
                        Children = { animation }
                    };
                    Storyboard.SetTarget(storyboard, presenter);
                    Storyboard.SetTargetProperty(storyboard, new PropertyPath(UIElement.OpacityProperty));
                    storyboard.Begin();
                }
            }
            base.PrepareContainerForItemOverride(element, item);
        }

        public bool IsNotAllItemsShown
        {
            get
            {
                return (bool)base.GetValue(IsNotAllItemsShownProperty);
            }
            set
            {
                base.SetValue(IsNotAllItemsShownProperty, value);
            }
        }
    }
}
