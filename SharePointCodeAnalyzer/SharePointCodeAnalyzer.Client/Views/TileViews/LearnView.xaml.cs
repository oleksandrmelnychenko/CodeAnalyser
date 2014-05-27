using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.TileViews
{
    /// <summary>
    /// Interaction logic for LearnView.xaml
    /// </summary>
    public partial class LearnView : ITileView
    {
        public LearnView()
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
