using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using Microsoft.AspNetCore.Components;
using DevExpress.ExpressApp.Utils;

namespace PP.NORE.Portal.Blazor.Server.Editors.CodeEditor
{
    public class EditorAdapter : ComponentAdapterBase
    {
        public EditorModel _model { get; }
        public EditorAdapter(EditorModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _model.Code = "";
			model.ValueChanged += ComponentModel_ValueChanged;
        }
 
        private void ComponentModel_ValueChanged(object sender, EventArgs e) => RaiseValueChanged();

        public override object GetValue()
        {
            return _model.Code;
        }

        public override void SetAllowEdit(bool allowEdit)
        {

        }

        public override void SetAllowNull(bool allowNull)
        {

        }

        public override void SetDisplayFormat(string displayFormat)
        {

        }

        public override void SetEditMask(string editMask)
        {

        }

        public override void SetEditMaskType(EditMaskType editMaskType)
        {

        }

        public override void SetErrorIcon(ImageInfo errorIcon)
        {

        }

        public override void SetErrorMessage(string errorMessage)
        {

        }

        public override void SetIsPassword(bool isPassword)
        {

        }

        public override void SetMaxLength(int maxLength)
        {

        }

        public override void SetNullText(string nullText)
        {

        }

        public override void SetValue(object value)
        {
            if (value != null)
            {
                _model.Code = value.ToString();
            }
        }

        protected override RenderFragment CreateComponent()
        {

            return ComponentModelObserver.Create(_model, EditorRenderer.Create(_model));
        }
        public override IComponentModel ComponentModel => _model;
    }
}
