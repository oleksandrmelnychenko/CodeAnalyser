using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class LoadingPanel : ContentControl
    {
        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(LoadingPanel), new PropertyMetadata(null, null));
        private Button closeButton;
        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(LoadingPanel), new PropertyMetadata(null, new PropertyChangedCallback(LoadingPanel.OnErrorChanged)));
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(LoadingPanel), new PropertyMetadata(false, new PropertyChangedCallback(LoadingPanel.OnIsBusyChanged)));
        public static readonly DependencyProperty IsBusyVisibilityProperty = DependencyProperty.Register("IsBusyVisibility", typeof(Visibility), typeof(LoadingPanel), new PropertyMetadata(Visibility.Collapsed, null));
        public static readonly DependencyProperty IsFailedProperty = DependencyProperty.Register("IsFailed", typeof(bool), typeof(LoadingPanel), new PropertyMetadata(false, new PropertyChangedCallback(LoadingPanel.OnIsFailedChanged)));
        public static readonly DependencyProperty IsFailedVisibilityProperty = DependencyProperty.Register("IsFailedVisibility", typeof(Visibility), typeof(LoadingPanel), new PropertyMetadata(Visibility.Collapsed, null));
        private Button sendButton;
        public static readonly DependencyProperty SendErrorCommandEmailProperty = DependencyProperty.Register("SendErrorCommandEmail", typeof(string), typeof(LoadingPanel), new PropertyMetadata("hello", null));
        public static readonly DependencyProperty SendErrorCommandProperty = DependencyProperty.Register("SendErrorCommand", typeof(ICommand), typeof(LoadingPanel), new PropertyMetadata(null, null));
        public static readonly DependencyProperty StatusMessageProperty = DependencyProperty.Register("StatusMessage", typeof(string), typeof(LoadingPanel), new PropertyMetadata(null, null));

        static LoadingPanel()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingPanel), new FrameworkPropertyMetadata(typeof(LoadingPanel)));
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsFailedVisibility = Visibility.Collapsed;
        }

        private void InternalOnApplyTemplate()
        {
            this.closeButton = base.GetTemplateChild("PART_CloseButton") as Button;
            this.closeButton.Click += new RoutedEventHandler(this.closeButton_Click);
            this.sendButton = base.GetTemplateChild("PART_SendButton") as Button;
            this.sendButton.Click += new RoutedEventHandler(this.closeButton_Click);
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
        }

        private static void OnErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LoadingPanel).IsBusy)
            {
                (d as LoadingPanel).IsBusyVisibility = Visibility.Visible;
            }
            else
            {
                (d as LoadingPanel).IsBusyVisibility = Visibility.Collapsed;
            }
        }

        private static void OnIsFailedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LoadingPanel).IsFailed)
            {
                (d as LoadingPanel).IsFailedVisibility = Visibility.Visible;
            }
            else
            {
                (d as LoadingPanel).IsFailedVisibility = Visibility.Collapsed;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return (ICommand)base.GetValue(CancelCommandProperty);
            }
            set
            {
                base.SetValue(CancelCommandProperty, value);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return (string)base.GetValue(ErrorMessageProperty);
            }
            set
            {
                base.SetValue(ErrorMessageProperty, value);
            }
        }

        public bool IsBusy
        {
            get
            {
                return (bool)base.GetValue(IsBusyProperty);
            }
            set
            {
                base.SetValue(IsBusyProperty, value);
            }
        }

        public Visibility IsBusyVisibility
        {
            get
            {
                return (Visibility)base.GetValue(IsBusyVisibilityProperty);
            }
            set
            {
                base.SetValue(IsBusyVisibilityProperty, value);
            }
        }

        public bool IsFailed
        {
            get
            {
                return (bool)base.GetValue(IsFailedProperty);
            }
            set
            {
                base.SetValue(IsFailedProperty, value);
            }
        }

        public Visibility IsFailedVisibility
        {
            get
            {
                return (Visibility)base.GetValue(IsFailedVisibilityProperty);
            }
            set
            {
                base.SetValue(IsFailedVisibilityProperty, value);
            }
        }

        public ICommand SendErrorCommand
        {
            get
            {
                return (ICommand)base.GetValue(SendErrorCommandProperty);
            }
            set
            {
                base.SetValue(SendErrorCommandProperty, value);
            }
        }

        public string SendErrorCommandEmail
        {
            get
            {
                return (string)base.GetValue(SendErrorCommandEmailProperty);
            }
            set
            {
                base.SetValue(SendErrorCommandEmailProperty, value);
            }
        }

        public string StatusMessage
        {
            get
            {
                return (string)base.GetValue(StatusMessageProperty);
            }
            set
            {
                base.SetValue(StatusMessageProperty, value);
            }
        }
    }
}
