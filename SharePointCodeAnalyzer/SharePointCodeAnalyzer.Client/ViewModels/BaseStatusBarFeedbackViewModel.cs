using System.Collections.Generic;
using SharePointCodeAnalyzer.CommonControls;

namespace SharePointCodeAnalyzer.Client.ViewModels
{
    public sealed class BaseStatusBarFeedbackViewModel : BaseViewModel
    {
        public List<StatusBarItem> FeedBack
        {
            get
            {
                return new List<StatusBarItem>
                {
                    new StatusBarItem("Send Feedback","Your opinion is needed"),
                };
            }
        } 

    }
}
