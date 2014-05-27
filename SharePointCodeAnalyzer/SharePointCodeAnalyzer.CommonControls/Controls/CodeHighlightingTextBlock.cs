using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using SharePointCodeAnalyzer.CommonControls.Core;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class CodeHighlightingTextBlock : ContentControl
    {
        private List<string> cSharpCodeIndicator = new List<string> { "public", "private", "void", "string", "int", "bool", "double", "List<", "IEnumerable<" };
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CodeHighlightingTextBlock), new PropertyMetadata(null, new PropertyChangedCallback(CodeHighlightingTextBlock.OnTextChanged)));

        private List<TextToken> FindCSharpTokens(string teststring)
        {
            List<TextToken> list = new List<TextToken>();
            int startPos = 0;
            while (startPos < teststring.Length)
            {
                int startIndex = this.IndexOfCSharpIndicator(teststring, startPos);
                if (startIndex >= startPos)
                {
                    if (startIndex > startPos)
                    {
                        TextToken token = new TextToken
                        {
                            Text = teststring.Substring(startPos, startIndex - startPos)
                        };
                        list.Add(token);
                    }
                    int num3 = 0;
                    string str = teststring.Substring(startIndex);
                    string str2 = "";
                    bool flag = false;
                    foreach (char ch in str)
                    {
                        str2 = str2 + ch;
                        switch (ch)
                        {
                            case '{':
                                flag = true;
                                num3++;
                                break;

                            case '}':
                                num3--;
                                break;
                        }
                        if (flag && (num3 == 0))
                        {
                            break;
                        }
                    }
                    CSharpCodeToken token2 = new CSharpCodeToken
                    {
                        Text = str2
                    };
                    list.Add(token2);
                    startPos = startIndex + str2.Length;
                    continue;
                }
                string str3 = teststring.Substring(startPos);
                TextToken item = new TextToken
                {
                    Text = str3
                };
                list.Add(item);
                startPos += str3.Length;
            }
            return list;
        }

        private int IndexOfCSharpIndicator(string teststring, int startPos)
        {
            int num = 0x7fffffff;
            foreach (string str in this.cSharpCodeIndicator)
            {
                string str2 = str + " ";
                int num2 = teststring.IndexOf(str2, startPos, StringComparison.CurrentCulture);
                if ((num2 >= 0) && (num2 < num))
                {
                    num = num2;
                }
            }
            if ((num >= 0) && (num != 0x7fffffff))
            {
                return num;
            }
            return -1;
        }

        private string MakeSafe(string text)
        {
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            text.Trim();
            return text;
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CodeHighlightingTextBlock).UpdateContent();
        }

        private List<TextToken> TokenizeString(string teststring)
        {
            List<TextToken> list = new List<TextToken>();
            int startIndex = 0;
            while (startIndex < teststring.Length)
            {
                int num2 = teststring.IndexOf("<", startIndex, StringComparison.CurrentCulture);
                if (num2 >= startIndex)
                {
                    if (num2 > startIndex)
                    {
                        TextToken item = new TextToken
                        {
                            Text = teststring.Substring(startIndex, num2 - startIndex)
                        };
                        list.Add(item);
                    }
                    int index = teststring.IndexOf(" ", num2);
                    if (index >= 0)
                    {
                        string str2 = "</" + teststring.Substring(num2 + 1, (index - num2) - 1).Trim() + ">";
                        int num4 = teststring.IndexOf(str2, num2);
                        if (num4 >= 0)
                        {
                            string str3 = teststring.Substring(num2, (num4 - num2) + str2.Length);
                            XmlCodeToken token2 = new XmlCodeToken
                            {
                                Text = str3
                            };
                            list.Add(token2);
                            startIndex = num4 + str2.Length;
                        }
                        else
                        {
                            string str4 = teststring.Substring(num2);
                            XmlCodeToken token3 = new XmlCodeToken
                            {
                                Text = str4
                            };
                            list.Add(token3);
                            startIndex = num2 + str4.Length;
                        }
                    }
                }
                else
                {
                    string str5 = teststring.Substring(startIndex);
                    TextToken token4 = new TextToken
                    {
                        Text = str5
                    };
                    list.Add(token4);
                    startIndex += str5.Length;
                }
            }
            List<TextToken> list2 = new List<TextToken>();
            foreach (TextToken token5 in list)
            {
                if (token5 is XmlCodeToken)
                {
                    list2.Add(token5);
                }
                else
                {
                    list2.AddRange(this.FindCSharpTokens(token5.Text));
                }
            }
            return list2;
        }

        private void UpdateContent()
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                string str = this.MakeSafe(this.Text);
                str = ("<Run>" + str).Replace("&lt;code&gt;", "</Run><Run FontFamily=\"Courier New\">").Replace("&lt;/code&gt;", "</Run><Run>").Replace("&lt;b&gt;", "</Run><Run FontWeight=\"Bold\">").Replace("&lt;/b&gt;", "</Run><Run FontFamily=\"Courier New\">") + "</Run>";
                object obj2 = XamlReader.Load(XmlReader.Create(new StringReader("<TextBlock Background=\"Transparent\" TextWrapping=\"Wrap\" xml:space=\"preserve\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" + str + "</TextBlock>")));
                base.Content = obj2;
            }
            else
            {
                base.Content = null;
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
