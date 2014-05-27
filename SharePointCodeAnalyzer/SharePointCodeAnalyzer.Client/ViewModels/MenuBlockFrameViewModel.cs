using System.Windows.Controls;
using SharePointCodeAnalyzer.Client.AppEngine.Views;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class MenuBlockFrameViewModel : BaseViewModel
    {
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

        UserControl _tiles;
        public UserControl Tiles
        {
            get { return _tiles; }
            set
            {
                _tiles = value;
                RaisePropertyChanged(() => Tiles);
            }
        }

        string _statusMessage;
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }

        /// <summary>
        ///     ctor.
        /// </summary>
        public MenuBlockFrameViewModel(ITileView tileControl)
        {
            PageTitle = tileControl.Title;
            StatusMessage = tileControl.StatusMessage;
            Tiles = tileControl as UserControl;
        }
    }
}
