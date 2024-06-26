using System;
using System.Reactive.Linq;
using Avalonia.Automation;
using Avalonia.Automation.Peers;

namespace Avalonia.Controls
{
    /// <summary>
    /// A selectable item in a <see cref="ComboBox"/>.
    /// </summary>
    public class ComboBoxItem : ListBoxItem
    {
        public ComboBoxItem()
        {
            this.GetObservable(ComboBoxItem.IsFocusedProperty).Where(focused => focused)
                .Subscribe(_ => (Parent as ComboBox)?.ItemFocused(this));
        }

        static ComboBoxItem()
        {
            AutomationProperties.ControlTypeOverrideProperty.OverrideDefaultValue<ComboBoxItem>(AutomationControlType.ComboBoxItem);
        }
    }
}
