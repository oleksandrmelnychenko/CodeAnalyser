using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.TileViews
{
    /// <summary>
    /// Interaction logic for ReviewView.xaml
    /// </summary>
    public partial class ReviewView : ITileView
    {
        public ReviewView()
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
