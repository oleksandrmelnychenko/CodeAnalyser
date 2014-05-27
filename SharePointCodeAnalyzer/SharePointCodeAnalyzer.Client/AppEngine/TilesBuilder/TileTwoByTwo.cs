using SharePointCodeAnalyzer.Client.AppEngine.TileCommonContracts;
using SharePointCodeAnalyzer.Client.AppEngine.Views;

namespace SharePointCodeAnalyzer.Client.AppEngine.TilesBuilder
{
    public sealed class TileTwoByTwo : ITileTwoByTwo
    {
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public ViewNavigationParameter NavParam { get; set; }
    }
}
