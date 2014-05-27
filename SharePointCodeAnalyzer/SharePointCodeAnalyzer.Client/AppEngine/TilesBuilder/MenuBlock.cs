using System.Collections.Generic;
using SharePointCodeAnalyzer.Client.AppEngine.TileCommonContracts;

namespace SharePointCodeAnalyzer.Client.AppEngine.TilesBuilder
{
    public class MenuBlock
    {
        private readonly string _title;
        private readonly string _subTitle;
        private readonly IList<ITile> _tiles;

        public string Title
        {
            get { return _title; }
        }

        public string SubTitle
        {
            get { return _subTitle; }
        }

        public IList<ITile> Tiles
        {
            get { return _tiles; }
        } 

        /// <summary>
        ///     ctor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="tiles"></param>
        public MenuBlock(string title, string subTitle, IList<ITile> tiles)
        {
            _title = title;
            _subTitle = subTitle;
            _tiles = tiles;
        }
    }
}
