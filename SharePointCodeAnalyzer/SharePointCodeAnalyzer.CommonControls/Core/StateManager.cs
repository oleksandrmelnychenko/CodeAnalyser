using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class StateManager : DependencyObject
    {
        private static Dictionary<FrameworkElement, string> cache = new Dictionary<FrameworkElement, string>();
        public static readonly DependencyProperty CurrentStateProperty = DependencyProperty.RegisterAttached("CurrentState", typeof(string), typeof(StateManager), new PropertyMetadata(null, new PropertyChangedCallback(StateManager.TransitionToState)));

        public static string GetCurrentState(DependencyObject obj)
        {
            return (string)obj.GetValue(CurrentStateProperty);
        }

        private static Storyboard GetStoryboard(DependencyObject sender, string stateName)
        {
            foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(sender as FrameworkElement))
            {
                foreach (VisualState state in group.States)
                {
                    if (state.Name == stateName)
                    {
                        return state.Storyboard;
                    }
                }
            }
            if (!(sender as FrameworkElement).IsLoaded)
            {
                cache.Add(sender as FrameworkElement, stateName);
                (sender as FrameworkElement).Loaded += new RoutedEventHandler(StateManager.StateManager_Loaded);
            }
            return null;
        }

        private static void GoToState(object sender, string newState)
        {
            Storyboard storyboard = GetStoryboard(sender as DependencyObject, newState);
            if (storyboard != null)
            {
                storyboard.Begin(sender as FrameworkElement);
            }
            else
            {
                VisualStateManager.GoToState(sender as FrameworkElement, newState, true);
            }
        }

        public static void SetCurrentState(DependencyObject obj, string value)
        {
            obj.SetValue(CurrentStateProperty, value);
        }

        private static void StateManager_Loaded(object sender, RoutedEventArgs e)
        {
            if (cache.ContainsKey(sender as FrameworkElement))
            {
                string newState = cache[sender as FrameworkElement];
                cache.Remove(sender as FrameworkElement);
                GoToState(sender as FrameworkElement, newState);
            }
        }

        private static void TransitionToState(object sender, DependencyPropertyChangedEventArgs args)
        {
            string str = args.NewValue.ToString();
            if (str.Contains("|"))
            {
                str = str.Substring(0, str.IndexOf("|"));
            }
            if (!string.IsNullOrEmpty(str))
            {
                GoToState(sender, str);
            }
        }
    }
}
