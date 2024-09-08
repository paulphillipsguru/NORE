using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Base;
using PP.NORE.Portal.Module.BusinessObjects.ProductSections;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;


[DefaultClassOptions]
public class Product(Session session) : XPObject(session)
{


    string validateMessage;
    FileData productPackage;
    FileData lookup;
    string status = ProductStatus.NotValidated;
    Product parentProduct;
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


    [Size(40)]
    public string Status
    {
        get => status;
        set => SetPropertyValue(nameof(Status), ref status, value);
    }

    [Size(255)]
    [RuleRequiredField]
    public string Name
    {
        get => name;
        set => SetPropertyValue(nameof(Name), ref name, value);
    }


    public Product ParentProduct
    {
        get => parentProduct;
        set => SetPropertyValue(nameof(ParentProduct), ref parentProduct, value);
    }

    [RuleRequiredField]
    public FileData DataModel
    {
        get => lookup;
        set => SetPropertyValue(nameof(DataModel), ref lookup, value);
    }
    [VisibleInDashboards(false)]
    [VisibleInDetailView(false)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    public FileData ProductPackage
    {
        get => productPackage;
        set => SetPropertyValue(nameof(ProductPackage), ref productPackage, value);
    }

    
    [Size(2000)]
    public string ValidateMessage
    {
        get => validateMessage;
        set => SetPropertyValue(nameof(ValidateMessage), ref validateMessage, value);
    }


    [Association("Product-Decisions")]
    public XPCollection<ProductDecision> Decisions
    {
        get
        {
            return GetCollection<ProductDecision>(nameof(Decisions));
        }
    }


    [Association("Product-Steps")]
	public XPCollection<Step> Steps
	{
		get
		{
			return GetCollection<Step>(nameof(Steps));
		}
	}

	[Association("Product-RequireLookups")]
	public XPCollection<Lookup> RequiredLookups
	{
		get
		{
			return GetCollection<Lookup>(nameof(RequiredLookups));
		}
	}

	[Association("Product-Packages")]
	public XPCollection<ProductPackage> Packages
	{
		get
		{
			return GetCollection<ProductPackage>(nameof(Packages));
		}
	}
    [Association("Product-Sections")]
    public XPCollection<ProductSection> Sections
    {
        get
        {
            return GetCollection<ProductSection>(nameof(Sections));
        }
    }
    [Association("Product-ProductTests")]
    public XPCollection<ProductTest> ProductTests
    {
        get
        {
            return GetCollection<ProductTest>(nameof(ProductTests));
        }
    }

    [Association("Product-BluePrints")]
    public XPCollection<CodeTemplate> BluePrints
    {
        get
        {
            return GetCollection<CodeTemplate>(nameof(BluePrints));
        }
    }




}