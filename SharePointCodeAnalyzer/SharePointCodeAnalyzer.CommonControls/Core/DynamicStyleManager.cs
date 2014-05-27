using System;
using System.Collections.Generic;
using System.Windows;
using SharePointCodeAnalyzer.CommonControls.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class DynamicStyleManager : DependencyObject
    {
        private static SnappedState appliedSnappedState = SnappedState.Unknown;
        private static List<DynamicStylableControl> snappedStylecontrols = new List<DynamicStylableControl>();
        public static readonly DependencyProperty SnappedStyleProperty = DependencyProperty.RegisterAttached("SnappedStyle", typeof(Style), typeof(DynamicStyleManager), new PropertyMetadata(null, new PropertyChangedCallback(DynamicStyleManager.RegisterSnappedStyleControl)));
        private static List<ThemableControl> themeChangecontrols = new List<ThemableControl>();
        public static readonly DependencyProperty UpdateOnThemeChangeProperty = DependencyProperty.RegisterAttached("UpdateOnThemeChange", typeof(bool), typeof(DynamicStyleManager), new PropertyMetadata(false, new PropertyChangedCallback(DynamicStyleManager.RegisterThemeChangeControl)));

        static DynamicStyleManager()
        {
            SnappedStateManager.SnappedStateChanged += new Action<SnappedState>(DynamicStyleManager.SnappedStateManager_SnappedStateChanged);
        }

        public static Style GetSnappedStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(SnappedStyleProperty);
        }

        public static bool GetUpdateOnThemeChange(DependencyObject obj)
        {
            return (bool)obj.GetValue(UpdateOnThemeChangeProperty);
        }

        private static void OnSnappedStateChanged(SnappedState newState)
        {
            CurrentState = newState;
            foreach (DynamicStylableControl control in snappedStylecontrols)
            {
                control.UpdateState(newState);
            }
            appliedSnappedState = newState;
        }

        private static void RegisterSnappedStyleControl(object sender, DependencyPropertyChangedEventArgs args)
        {
            object newValue = args.NewValue;
            Style style = (sender as FrameworkElement).Style;
        }

        private static void RegisterThemeChangeControl(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is FrameworkElement)
            {
                themeChangecontrols.Add(new ThemableControl(sender as FrameworkElement));
            }
        }

        public static void SetSnappedStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(SnappedStyleProperty, value);
        }

        public static void SetUpdateOnThemeChange(DependencyObject obj, bool value)
        {
            obj.SetValue(UpdateOnThemeChangeProperty, value);
        }

        private static void SnappedStateManager_SnappedStateChanged(SnappedState currentSnappedState)
        {
            if (appliedSnappedState != currentSnappedState)
            {
                OnSnappedStateChanged(currentSnappedState);
            }
        }

        public static SnappedState CurrentState { get; set; }
    }
}
