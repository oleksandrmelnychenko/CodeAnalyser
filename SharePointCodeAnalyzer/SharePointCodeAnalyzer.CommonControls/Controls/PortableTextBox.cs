using System.Windows.Controls;
using System.Windows.Data;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    public class PortableTextBox : TextBox
    {
        public PortableTextBox()
        {
            base.TextChanged += new TextChangedEventHandler(this.PortableTextBox_TextChanged);
        }

        private void PortableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bindingExpression = base.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}
