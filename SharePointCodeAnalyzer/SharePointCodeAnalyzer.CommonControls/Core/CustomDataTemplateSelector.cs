using System.Windows;
using System.Windows.Controls;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public class CustomDataTemplateSelector : ContentControl
    {
        public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register("Prefix", typeof(string), typeof(CustomDataTemplateSelector), new PropertyMetadata(null, new PropertyChangedCallback(CustomDataTemplateSelector.OnPrefixChanged)));
        public bool reloadTemplate;
        public static readonly DependencyProperty SuffixProperty = DependencyProperty.Register("Suffix", typeof(string), typeof(CustomDataTemplateSelector), new PropertyMetadata(null, new PropertyChangedCallback(CustomDataTemplateSelector.OnPrefixChanged)));

        public CustomDataTemplateSelector()
        {
            base.VerticalAlignment = VerticalAlignment.Stretch;
            base.HorizontalAlignment = HorizontalAlignment.Stretch;
            base.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            try
            {
                if (this.reloadTemplate)
                {
                    base.ContentTemplate = this.SelectTemplate(base.Content, this);
                    this.reloadTemplate = false;
                }
                return base.MeasureOverride(constraint);
            }
            catch
            {
            }
            return new Size(0.0, 0.0);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.UpdateContentTempate();
        }

        private static void OnPrefixChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CustomDataTemplateSelector).UpdateContentTempate();
        }

        public DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is string)
            {
                string key = this.Prefix + "_" + item.ToString();
                if (!string.IsNullOrEmpty(this.Suffix))
                {
                    key = key + "_" + this.Suffix;
                }
                return (ResourceHelper.FindResource(key) as DataTemplate);
            }
            string text1 = this.Prefix + "_";
            string suffix = null;
            if (!string.IsNullOrEmpty(this.Suffix))
            {
                suffix = "_" + this.Suffix;
            }
            return (ResourceHelper.GetResourceOfType(item.GetType(), null, this.Prefix + "_", suffix) as DataTemplate);
        }

        private void UpdateContentTempate()
        {
            if ((base.Content != null) && !string.IsNullOrEmpty(this.Prefix))
            {
                this.reloadTemplate = true;
            }
        }

        public string Prefix
        {
            get
            {
                return (string)base.GetValue(PrefixProperty);
            }
            set
            {
                base.SetValue(PrefixProperty, value);
            }
        }

        public string Suffix
        {
            get
            {
                return (string)base.GetValue(SuffixProperty);
            }
            set
            {
                base.SetValue(SuffixProperty, value);
            }
        }
    }
}
