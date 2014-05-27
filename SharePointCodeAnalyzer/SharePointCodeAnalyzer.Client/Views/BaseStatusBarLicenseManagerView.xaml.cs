using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
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
