using System;
using System.Collections.Generic;

namespace SharePointCodeAnalyzer.Client.AppEngine.Views
{
    internal sealed class ViewsContainer
    {
        private readonly Dictionary<ViewNavigationParameter, Func<IView>> _views;

        public ViewsContainer()
        {
            //_views = new Dictionary<ViewNavigationParameter, Func<IView>>
            //{
            //    {
            //        ViewNavigationParameter.Shell,
            //        ()=> new ViewFactory<ShellView>().View
            //    }
            //};
        }

    }
}
