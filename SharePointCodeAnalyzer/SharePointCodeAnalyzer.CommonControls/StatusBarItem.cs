
namespace SharePointCodeAnalyzer.CommonControls
{
    public sealed class StatusBarItem
    {
        private readonly string _displayName;
        private readonly string _statusMessage;

        public string DisplayName
        {
            get { return _displayName; }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
        }

        public StatusBarItem(string displayName, string statusMessage)
        {
            _displayName = displayName;
            _statusMessage = statusMessage;
        }
    }
}
