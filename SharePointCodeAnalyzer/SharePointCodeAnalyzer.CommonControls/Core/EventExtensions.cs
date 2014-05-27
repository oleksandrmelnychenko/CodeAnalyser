using System;
using System.Diagnostics;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public static class EventExtensions
    {
        [DebuggerStepThrough, DebuggerNonUserCode]
        public static void Raise<TEventArgs>(this Action<TEventArgs> handler, TEventArgs args)
        {
            if (handler != null)
            {
                handler(args);
            }
        }

        [DebuggerStepThrough, DebuggerNonUserCode]
        public static void Raise<TEventArg1, TEventArg2>(this Action<TEventArg1, TEventArg2> handler, TEventArg1 arg1, TEventArg2 arg2)
        {
            if (handler != null)
            {
                handler(arg1, arg2);
            }
        }

        [DebuggerNonUserCode, DebuggerStepThrough]
        public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object owner, TEventArgs args) where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler(owner, args);
            }
        }
    }
}
