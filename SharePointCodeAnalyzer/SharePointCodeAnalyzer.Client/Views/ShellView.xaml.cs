using SharePointCodeAnalyzer.Client.AppEngine.Views;
using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : IView
    {
        public ShellView()
        {
            InitializeComponent();
            DataContext = new ShellViewModel();
        }
    }
}
