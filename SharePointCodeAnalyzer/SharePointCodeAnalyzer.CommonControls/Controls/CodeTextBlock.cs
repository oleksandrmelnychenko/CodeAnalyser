using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class CodeTextBlock : ContentControl
    {
        public static readonly DependencyProperty LineNumberProperty = DependencyProperty.Register("LineNumber", typeof(int), typeof(CodeTextBlock), new PropertyMetadata(0, new PropertyChangedCallback(CodeTextBlock.OnTextChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CodeTextBlock), new PropertyMetadata(null, new PropertyChangedCallback(CodeTextBlock.OnTextChanged)));

        public CodeTextBlock()
        {
            base.HorizontalAlignment = HorizontalAlignment.Stretch;
            base.VerticalAlignment = VerticalAlignment.Stretch;
            base.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            base.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        private string MakeSafe(string text, int linenumber, int maxnumber)
        {
            text = text.Replace("\t", "  ");
            text = text.Replace("&", "&amp;");
            text = text.Replace("\"", "&quot;");
            text = text.Replace("'", "&apos;");
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            return (linenumber.ToString().PadLeft(maxnumber.ToString().Length, ' ') + " " + text);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CodeTextBlock).UpdateContent();
        }

        private void UpdateContent()
        {
            double actualWidth = base.ActualWidth;
            if (!string.IsNullOrEmpty(this.Text))
            {
                string str = "";
                string[] strArray = this.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                int linenumber = 1;
                foreach (string str2 in strArray)
                {
                    if (linenumber == this.LineNumber)
                    {
                        str = (str + "<Bold Background=\"Yellow\">" + this.MakeSafe(str2, linenumber, strArray.Length) + "</Bold>") + "<LineBreak />";
                    }
                    else
                    {
                        str = str + this.MakeSafe(str2, linenumber, strArray.Length) + "<LineBreak />";
                    }
                    linenumber++;
                }
                try
                {
                    object obj2 = null;
                    obj2 = XamlReader.Load(XmlReader.Create(new StringReader("<RichTextBox VerticalScrollBarVisibility=\"Auto\" HorizontalScrollBarVisibility=\"Auto\" BorderThickness=\"0\" BorderBrush=\"Transparent\" Background=\"Transparent\" xml:space=\"preserve\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><FlowDocument PageWidth=\"6000\"><Paragraph>" + str + "</Paragraph></FlowDocument></RichTextBox>")));
                    Control control1 = obj2 as Control;
                    base.Content = obj2;
                }
                catch (Exception)
                {
                    base.Content = this.Text;
                }
            }
            else
            {
                base.Content = null;
            }
        }

        public int LineNumber
        {
            get
            {
                return (int)base.GetValue(LineNumberProperty);
            }
            set
            {
                base.SetValue(LineNumberProperty, value);
            }
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
    }
}
