using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.TileViews
{
    /// <summary>
    ///     Interaction logic for AnalyseView.xaml
    /// </summary>
    public partial class AnalyseView : ITileView
    {
        public AnalyseView()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return "ANALYSE"; }
        }

        public string StatusMessage
        {
            get { return "Analyse you SharePoint solutions packages"; }
        }
    }
}
