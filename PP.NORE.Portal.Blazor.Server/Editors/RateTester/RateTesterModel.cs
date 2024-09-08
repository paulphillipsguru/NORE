using DevExpress.ExpressApp.Blazor.Components.Models;
using PP.NORE.Shared.Contracts;
using PP.NORE.Shared.Models;

namespace PP.NORE.Portal.Blazor.Server.Editors.RateTester
{
	public class RateTesterModel : ComponentModelBase
	{
		public Dictionary<string, string> TestData
		{
			get => GetPropertyValue<Dictionary<string, string>>();
			set => SetPropertyValue(value);

		}

		public RateRequest RateRequest
		{
            get => GetPropertyValue<RateRequest>();
            set => SetPropertyValue(value);
        }

		public IProductRater Rater
		{
			get => GetPropertyValue<IProductRater>();
			set => SetPropertyValue(value);
		}
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
