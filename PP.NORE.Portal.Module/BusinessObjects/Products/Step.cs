using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Base;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;

[DefaultClassOptions]
[ImageName("ContextMenu_Sort_Descending")]
public class Step(Session session) : XPObject(session)
{
	string validationMessage;
	string status;
	
	Product product;
	string algorithm;
	string code;

	[Size(255)]
	[RuleRequiredField]
	public string Code
	{
		get => code;
		set => SetPropertyValue(nameof(Code), ref code, value);
	}


	[Size(SizeAttribute.Unlimited)]
	[RuleRequiredField]
	public string Algorithm
	{
		get => algorithm;
		set => SetPropertyValue(nameof(Algorithm), ref algorithm, value);
	}

	[Size(50)]
	public string Status
	{
		get => status;
		set => SetPropertyValue(nameof(Status), ref status, value);
	}


	[Size(2000)]
	public string ValidationMessage
	{
		get => validationMessage;
		set => SetPropertyValue(nameof(ValidationMessage), ref validationMessage, value);
	}


	[Association("Step-Factors")]
	public XPCollection<Factor> Factors
	{
		get
		{
			return GetCollection<Factor>(nameof(Factors));
		}
	}

	[Association("Product-Steps")]
	[VisibleInDashboards(false)]
	[VisibleInDetailView(false)]
	[VisibleInListView(false)]
	[VisibleInLookupListView(false)]
	public Product Product
	{
		get => product;
		set => SetPropertyValue(nameof(Product), ref product, value);
	}




}