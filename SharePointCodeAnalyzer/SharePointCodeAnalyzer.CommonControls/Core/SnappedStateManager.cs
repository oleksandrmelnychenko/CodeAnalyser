using System;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public static class SnappedStateManager
    {
        private static double h = 0.0;
        private static double w = 0.0;

        public static event Action<SnappedState> SnappedStateChanged;

        public static void UpdateSize(double width, double height)
        {
            w = width;
            h = height;
            if ((width > 0.0) && (width <= 330.0))
            {
                SnappedStateChanged.Raise<SnappedState>(SnappedState.Snapped);
            }
            else
            {
                SnappedStateChanged.Raise<SnappedState>(SnappedState.FullScreen);
            }
        }
    }
}
