using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace PP.NORE.Portal.Blazor.Server.Editors.CodeEditor
{
    [PropertyEditor(typeof(string), false)]
    public class EditorProperty : BlazorPropertyEditorBase
    {

        public EditorProperty(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
        }

        protected override IComponentAdapter CreateComponentAdapter()
            => new EditorAdapter(new EditorModel());
    }
}
