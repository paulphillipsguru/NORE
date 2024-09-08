using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Products;


namespace PP.NORE.Portal.Module.BusinessObjects.Base;
[DefaultClassOptions]
[NavigationItem("Library")]
[ImageName("Navigation_Item_View")]
public class Lookup(Session session) : XPObject(session)
{

	Product product;
	
	string name;
	string code;

	[Size(255)]
	[RuleRequiredField]
	[RuleUniqueValue]
	public string Code
	{
		get => code;
		set => SetPropertyValue(nameof(Code), ref code, value);
	}


	[Size(255)]
	[RuleRequiredField]
	public string Name
	{
		get => name;
		set => SetPropertyValue(nameof(Name), ref name, value);
	}

	

	[Association("Product-RequireLookups")]
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