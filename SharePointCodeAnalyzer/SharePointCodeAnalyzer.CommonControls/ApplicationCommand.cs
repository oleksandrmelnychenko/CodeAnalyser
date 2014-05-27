using System;
using System.Windows.Input;

namespace SharePointCodeAnalyzer.CommonControls
{
    public sealed class ApplicationCommand
    {
        private readonly Action _commandAction;
        private readonly Action _canExecuteCommandAction;

        public char Icon { get; set; }
        public string DisplayName { get; set; }

        public ICommand ExecuteCommand { get; set; }

        public ApplicationCommand(Action commandAction, Action canExecuteCommandAction, string displayName, char icon)
        {
            Icon = icon;

            _commandAction = commandAction;
            _canExecuteCommandAction = canExecuteCommandAction;
            DisplayName = displayName;
        }
    }
}
