using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class PortablePopup : ContentControl
    {
        private ContentControl contentControl;
        public static readonly DependencyProperty HideOnPropertyChangeProperty = DependencyProperty.Register("HideOnPropertyChange", typeof(object), typeof(PortablePopup), new PropertyMetadata(false, new PropertyChangedCallback(PortablePopup.OnPropertyChanged)));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(PortablePopup), new PropertyMetadata(false, new PropertyChangedCallback(PortablePopup.OnIsOpenChanged)));
        private Popup popup;

        static PortablePopup()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PortablePopup), new FrameworkPropertyMetadata(typeof(PortablePopup)));
        }

        private void ClosePopup()
        {
            base.SetValue(IsOpenProperty, false);
        }

        private void contentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.SetValue(IsOpenProperty, false);
        }

        private void HidePopup()
        {
            if (this.popup != null)
            {
                this.ClosePopup();
            }
        }

        public void InternalOnApplyTemplate()
        {
            this.popup = base.GetTemplateChild("PART_Popup") as Popup;
            this.contentControl = base.GetTemplateChild("PART_Content") as ContentControl;
            this.popup.StaysOpen = false;
            this.popup.PreviewMouseUp += new MouseButtonEventHandler(this.PopupOnMouseLeftButtonUp);
            this.popup.MouseLeftButtonUp += new MouseButtonEventHandler(this.PopupOnMouseLeftButtonUp);
            base.OnApplyTemplate();
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((e.NewValue as bool?) == true)
            {
                (d as PortablePopup).OnPopupOpened();
            }
            else
            {
                (d as PortablePopup).OnPopupClosed();
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.SetValue(IsOpenProperty, false);
        }

        private void OnPopupClosed()
        {
        }

        private void OnPopupOpened()
        {
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PortablePopup).HidePopup();
        }

        private void PopupOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (!(mouseButtonEventArgs.OriginalSource is TextBox))
            {
                base.SetValue(IsOpenProperty, false);
            }
        }

        private void TheStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is TextBox))
            {
                base.SetValue(IsOpenProperty, false);
            }
        }

        public object HideOnPropertyChange
        {
            get
            {
                return base.GetValue(HideOnPropertyChangeProperty);
            }
            set
            {
                base.SetValue(HideOnPropertyChangeProperty, value);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)base.GetValue(IsOpenProperty);
            }
            set
            {
                base.SetValue(IsOpenProperty, value);
            }
        }
    }
}
