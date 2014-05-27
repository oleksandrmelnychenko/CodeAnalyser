using System.Collections.Generic;
using System.Windows.Controls;
using SharePointCodeAnalyzer.Client.AppEngine.Navigation;
using SharePointCodeAnalyzer.Client.Views;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class MainWindowViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public List<ApplicationCommand> SystemCommands
        {
            get { return BuildSystemCommands(); }
        }

        public List<UserControl> StatusBar { get; set; }

        public UserControl MainContent { get; set; }

        /// <summary>
        ///     ctor.
        /// </summary>
        public MainWindowViewModel()
        {
            _navigationService = new NavigationService();

            StatusBar = new List<UserControl>
            {
                new BaseStatusBarConnectionManagerView(),
                new BaseStatusBarLicenseManagerView(),
                new BaseStatusBarFeedbackView()
            };

            MainContent = new MainFrameView();

            RaisePropertyChanged(() => MainContent);
            RaisePropertyChanged(() => StatusBar);
        }

        private List<ApplicationCommand> BuildSystemCommands()
        {
            return new List<ApplicationCommand>
                {
                    new ApplicationCommand(DoCommand, DoCommand, "Help", '?'),
                    new ApplicationCommand(DoCommand, DoCommand, "Home", ''),
                    new ApplicationCommand(DoCommand, DoCommand, "Settings", ''),
                    new ApplicationCommand(DoCommand, DoCommand, "About", '')
                };
        }

        public void DoCommand()
        {

        }
    }
}
