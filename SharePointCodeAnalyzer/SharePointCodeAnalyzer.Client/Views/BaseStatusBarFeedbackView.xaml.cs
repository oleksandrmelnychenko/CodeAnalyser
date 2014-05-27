using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    /// Interaction logic for BaseStatusBarFeedbackView.xaml
    /// </summary>
    public partial class BaseStatusBarFeedbackView
    {
        public BaseStatusBarFeedbackView()
        {
            InitializeComponent();
            DataContext = new BaseStatusBarFeedbackViewModel();
        }
    }
}
