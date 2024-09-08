using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Products;

namespace PP.NORE.Portal.Module.BusinessObjects.Base;

[DefaultClassOptions]
[NavigationItem("Library")]
public class CodeTemplate(Session session) : XPObject(session)
{
    bool isDefault;
    Product product;
    bool isDataTemplate;

    string template;
    string code;

    [Size(255)]
    [RuleRequiredField]
    [RuleUniqueValue]

    public string Code
    {
        get => code;
        set => SetPropertyValue(nameof(Code), ref code, value);
    }
    [VisibleInDashboards(false)]
    [VisibleInDetailView(true)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    public bool IsDataTemplate
    {
        get => isDataTemplate;
        set => SetPropertyValue(nameof(IsDataTemplate), ref isDataTemplate, value);
    }

    [Size(SizeAttribute.Unlimited)]
    [RuleRequiredField]
    public string Template
    {
        get => template;
        set => SetPropertyValue(nameof(Template), ref template, value);
    }

    
    public bool IsDefault
    {
        get => isDefault;
        set => SetPropertyValue(nameof(IsDefault), ref isDefault, value);
    }

    [Association("Product-BluePrints")]
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