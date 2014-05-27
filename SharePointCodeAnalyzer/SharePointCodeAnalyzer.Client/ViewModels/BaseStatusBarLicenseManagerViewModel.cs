using System.Collections.Generic;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class BaseStatusBarLicenseManagerViewModel : BaseViewModel
    {
        public List<StatusBarItem> LicenseManager
        {
            get
            {
                return new List<StatusBarItem>
                {
                     new StatusBarItem("License","Trial until 6/18/2014 (20 runs left)"),
                };
            }
        } 
    }
}
