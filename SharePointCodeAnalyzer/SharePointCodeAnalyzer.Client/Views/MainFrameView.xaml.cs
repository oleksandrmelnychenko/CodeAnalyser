using SharePointCodeAnalyzer.Client.ViewModels;

namespace SharePointCodeAnalyzer.Client.Views
{
    /// <summary>
    /// Interaction logic for MainFrameView.xaml
    /// </summary>
    public partial class MainFrameView 
    {
        public MainFrameView()
        {
            InitializeComponent();
            DataContext = new MainFrameViewModel();
        }
    }
}
