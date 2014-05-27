using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views.TitleBarViews
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
