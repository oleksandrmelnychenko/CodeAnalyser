using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.LearnViews
{
    /// <summary>
    /// Interaction logic for LearnView.xaml
    /// </summary>
    public partial class ShellLearnView : ITileView
    {
        public ShellLearnView()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return "LEARN"; }
        }

        public string StatusMessage
        {
            get { return "Learn how to improve your SharePoint Code Quality"; }
        }
    }
}
