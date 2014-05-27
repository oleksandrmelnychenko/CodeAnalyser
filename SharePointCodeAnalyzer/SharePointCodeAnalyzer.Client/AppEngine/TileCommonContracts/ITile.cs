using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.AppEngine.TileCommonContracts
{
    public interface ITile
    {
        string ImageUrl { get; set; }
        string Title { get; set; }
        ViewNavigationParameter NavParam { get; set; }
    }
}
