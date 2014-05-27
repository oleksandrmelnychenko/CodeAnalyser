using SharePointCodeAnalyzer.Client.AppEngine.Views;
using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    /// Interaction logic for MenuBlockFrame.xaml
    /// </summary>
    public partial class MenuBlockFrameView 
    {
        public MenuBlockFrameView(ITileView tileControl)
        {
            InitializeComponent();
            DataContext = new MenuBlockFrameViewModel(tileControl);
        }
    }
}
