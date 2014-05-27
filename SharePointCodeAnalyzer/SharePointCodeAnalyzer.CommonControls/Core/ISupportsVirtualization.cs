using System;
using System.Windows;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    public interface ISupportsVirtualization
    {
        event Action<ISupportsVirtualization, bool> OnNotAllItemsShowChanged;

        int CalculateMaxVisibleItems(Size assumedItemSize);

        System.Windows.Controls.ItemContainerGenerator ItemContainerGenerator { get; set; }
    }
}
