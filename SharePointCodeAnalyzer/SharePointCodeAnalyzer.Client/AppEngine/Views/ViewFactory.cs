
namespace SharePointCodeAnalyzer.Client.AppEngine.Views
{
    internal sealed class ViewFactory<TView> where TView : IView, new()
    {
        private readonly TView _view;

        public TView View
        {
            get { return _view; }
        }

        public ViewFactory()
        {
            _view = new TView();
        }
    }
}
