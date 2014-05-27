using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.AppEngine.Navigation
{
    public interface INavigationService
    {
        void GoBack();
        void SaveViewInHistory(IView view);
    }
}
