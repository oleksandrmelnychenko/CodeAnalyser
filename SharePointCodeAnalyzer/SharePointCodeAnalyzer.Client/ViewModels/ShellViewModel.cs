using System.Collections.Generic;
using System.Collections.ObjectModel;
using SharePointCodeAnalyzer.Client.AppEngine.Views;
using SharePointCodeAnalyzer.Client.Views;
using SharePointCodeAnalyzer.Client.Views.AnalyseViews;
using SharePointCodeAnalyzer.Client.Views.LearnViews;
using SharePointCodeAnalyzer.Client.Views.ReviewViews;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class ShellViewModel : BaseViewModel, IShell
    {
        public ObservableCollection<MenuBlockFrameView> MenuBlocks { get; set; }

        /// <summary>
        ///     ctor.
        /// </summary>
        public ShellViewModel()
        {
            MenuBlocks = new ObservableCollection<MenuBlockFrameView>(BuildHomeMenu());
        }

        private List<MenuBlockFrameView> BuildHomeMenu()
        {
            return new List<MenuBlockFrameView>
            {
                new MenuBlockFrameView(new ShellAnalyseView()),
                new MenuBlockFrameView(new ShellReviewView()),
                new MenuBlockFrameView(new ShellLearnView()),
            };
        }

        public void BuildShell()
        {

        }
    }
}
