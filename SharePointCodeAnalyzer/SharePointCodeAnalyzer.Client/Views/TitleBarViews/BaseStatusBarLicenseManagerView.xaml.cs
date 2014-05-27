using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views.TitleBarViews
{
    /// <summary>
    /// Interaction logic for BaseStatusBarLicenseManagerView.xaml
    /// </summary>
    public partial class BaseStatusBarLicenseManagerView 
    {
        public BaseStatusBarLicenseManagerView()
        {
            InitializeComponent();
            DataContext = new BaseStatusBarLicenseManagerViewModel();
        }
    }
}
