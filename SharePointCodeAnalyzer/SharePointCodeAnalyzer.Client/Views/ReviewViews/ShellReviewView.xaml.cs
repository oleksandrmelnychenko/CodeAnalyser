using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.ReviewViews
{
    /// <summary>
    /// Interaction logic for ReviewView.xaml
    /// </summary>
    public partial class ShellReviewView : ITileView
    {
        public ShellReviewView()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return "REVIEW"; }
        }

        public string StatusMessage
        {
            get { return "Review code analysis results"; }
        }
    }
}
