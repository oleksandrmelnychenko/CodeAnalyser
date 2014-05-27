using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    ///     Interaction logic for BaseStatusBarConnectionManagerView.xaml
    /// </summary>
    public partial class BaseStatusBarConnectionManagerView 
    {
        public BaseStatusBarConnectionManagerView()
        {
            InitializeComponent();
            DataContext = new BaseStatusBarConnectionManager();
        }
    }
}
