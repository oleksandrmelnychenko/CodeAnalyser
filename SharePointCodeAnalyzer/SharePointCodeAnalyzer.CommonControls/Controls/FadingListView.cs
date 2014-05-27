using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class FadingListView : ListBox
    {
        public FadingListView()
        {
            base.Loaded += new RoutedEventHandler(this.FadingListView_Loaded);
        }

        private void FadingListView_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateHasItems();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            this.UpdateHasItems();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            int num = base.ItemContainerGenerator.IndexFromContainer(element);
            ListBoxItem item2 = (ListBoxItem)element;
            item2.Opacity = 0.0;
            TimeSpan span = TimeSpan.FromMilliseconds(num * (300.0 / ((double)base.Items.Count)));

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(500.0),
                BeginTime = new TimeSpan?(span)
            };

            Storyboard storyboard = new Storyboard
            {
                Children = { animation }
            };

            Storyboard.SetTarget(storyboard, item2);
            Storyboard.SetTargetProperty(storyboard, new PropertyPath(UIElement.OpacityProperty));
            storyboard.Begin();
            storyboard.Begin();
            base.PrepareContainerForItemOverride(element, item);
        }

        private void UpdateHasItems()
        {
            if (base.Items.Count > 0)
            {
                VisualStateManager.GoToState(this, "HasItems", false);
            }
        }
    }
}
