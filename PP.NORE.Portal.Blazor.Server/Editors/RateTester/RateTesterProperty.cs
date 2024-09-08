using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;


namespace PP.NORE.Portal.Blazor.Server.Editors.RateTester
{
	[PropertyEditor(typeof(FileData), false)]
	public class RateTesterProperty : BlazorPropertyEditorBase
	{

		public RateTesterProperty(Type objectType, IModelMemberViewItem model)
			: base(objectType, model)
		{
		}

		protected override IComponentAdapter CreateComponentAdapter()
			=> new RateTesterAdapter(new RateTesterModel());
	}
}
