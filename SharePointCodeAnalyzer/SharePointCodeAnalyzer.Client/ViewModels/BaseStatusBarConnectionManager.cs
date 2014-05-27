using System.Collections.Generic;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class BaseStatusBarConnectionManager : BaseViewModel
    {
        public List<StatusBarItem> BaseStatus
        {
            get
            {
                return new List<StatusBarItem>
                {
                    new StatusBarItem("Status", "Ready"),
                };
            }
        } 
    }
}
