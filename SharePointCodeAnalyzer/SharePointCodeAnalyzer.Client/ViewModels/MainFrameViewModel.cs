using System.Windows.Controls;
using SharePointCodeAnalyzer.Client.Views;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    internal sealed class MainFrameViewModel : BaseViewModel
    {
        #region Bindable properties

        /// <summary>
        ///     Diplay GoBack button.
        /// </summary>
        bool _canGoBack;
        public bool CanGoBack
        {
            get { return _canGoBack; }
            set
            {
                _canGoBack = value;
                RaisePropertyChanged(() => CanGoBack);
            }
        }

        /// <summary>
        ///     Display view in main frame.
        /// </summary>
        UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged(() => CurrentView);
            }
        }

        /// <summary>
        ///     Is not home hander.
        /// </summary>
        bool _isNotHome;
        public bool IsNotHome
        {
            get { return _isNotHome; }
            set
            {
                _isNotHome = value;
                RaisePropertyChanged(() => IsNotHome);
            }
        }

        /// <summary>
        ///     Page title.
        /// </summary>
        string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged(() => PageTitle);
            }
        }

        #endregion

        /// <summary>
        ///     ctor.
        /// </summary>
        public MainFrameViewModel()
        {
            CurrentView = new ShellView();
        }
    }
}
