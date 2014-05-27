using SharePointCodeAnalyzer.Client.AppEngine.TileCommonContracts;
using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.AppEngine.TilesBuilder
{
    public class TileTwoByOne : ITileTwoByOne
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public ViewNavigationParameter NavParam { get; set; }
    }
}
