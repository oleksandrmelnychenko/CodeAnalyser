using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharePointCodeAnalyzer.Client.AppEngine.Views;
using SharePointCodeAnalyzer.Client.Views;
using SharePointCodeAnalyzer.Client.Views.TileViews;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class HomeViewModel : BaseViewModel, IShell
    {
        public ObservableCollection<MenuBlockFrameView> MenuBlocks { get; set; }

        /// <summary>
        ///     ctor.
        /// </summary>
        public HomeViewModel()
        {
            MenuBlocks = new ObservableCollection<MenuBlockFrameView>(BuildHomeMenu());
        }

        private List<MenuBlockFrameView> BuildHomeMenu()
        {
            return new List<MenuBlockFrameView>
            {
                new MenuBlockFrameView(new AnalyseView()),
                new MenuBlockFrameView(new ReviewView()),
                new MenuBlockFrameView(new LearnView()),
            };
        }
    }
}
