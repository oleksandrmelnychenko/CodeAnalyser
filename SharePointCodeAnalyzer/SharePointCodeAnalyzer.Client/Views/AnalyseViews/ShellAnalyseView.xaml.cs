using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.Views.AnalyseViews
{
    /// <summary>
    ///     Interaction logic for AnalyseView.xaml
    /// </summary>
    public partial class ShellAnalyseView : ITileView
    {
        public ShellAnalyseView()
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
