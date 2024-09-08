using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.BaseImpl;
using Microsoft.AspNetCore.Components;
using PP.NORE.RatingEngine;
using PP.NORE.Shared.Cache;
using PP.NORE.Shared.Models;
using System.Text.Json;

namespace PP.NORE.Portal.Blazor.Server.Editors.RateTester
{
	public class RateTesterAdapter : ComponentAdapterBase
	{

		public RateTesterModel _model { get; }
		public RateTesterAdapter(RateTesterModel model)
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
			
				var answers = new Dictionary<string, JsonElement>();

				_model.RateRequest = new RateRequest() { Entity = "", InstanceName = "", ProductCode = "", Answers=answers };
				var productBundle = value as FileData;
				productBundle.SaveToStream(_model.RateRequest.ProductBinary);

				_model.Rater = RuleCache.CreateProductRaterFromBinary(_model.RateRequest);
			}
		}

		protected override RenderFragment CreateComponent()
		{

			return ComponentModelObserver.Create(_model, RateTesterRenderer.Create(_model));
		}
		public override IComponentModel ComponentModel => _model;

	}
}
