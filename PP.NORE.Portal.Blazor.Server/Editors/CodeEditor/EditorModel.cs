using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace PP.NORE.Portal.Blazor.Server.Editors.CodeEditor
{
    public class EditorModel : ComponentModelBase
    {
        public string Code
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public void SetValueFromUI(string value)
        {
            SetPropertyValue(value, notify: false, nameof(Code));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;
    }
}

