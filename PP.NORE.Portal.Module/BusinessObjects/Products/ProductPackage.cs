using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;

[DefaultClassOptions]

[ImageName("LayoutCustomization_Group_Hidden")]
public class ProductPackage(Session session) : XPObject(session)
{

    Product product;
    DateTime lastUpdateDate;
    DateTime effectiveTo;
    DateTime effectiveFrom;
    FileData productBundle;

    [VisibleInDashboards(false)]
    [VisibleInDetailView(false)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]

    public FileData Package
    {
        get => productBundle;
        set => SetPropertyValue(nameof(Package), ref productBundle, value);
    }

    [RuleRequiredField]
    public DateTime LastUpdateDate
    {
        get => lastUpdateDate;
        set => SetPropertyValue(nameof(LastUpdateDate), ref lastUpdateDate, value);
    }

    [RuleRequiredField]
    public DateTime EffectiveFrom
    {
        get => effectiveFrom;
        set => SetPropertyValue(nameof(EffectiveFrom), ref effectiveFrom, value);
    }


    public DateTime EffectiveTo
    {
        get => effectiveTo;
        set => SetPropertyValue(nameof(EffectiveTo), ref effectiveTo, value);
    }


    [Association("Product-Packages")]
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