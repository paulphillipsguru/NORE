using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Products;

namespace PP.NORE.Portal.Module.BusinessObjects.ProductSections;

[DefaultClassOptions]
[ImageName("LayoutCustomization_TextPosition_Left")]

public class ProductSection(Session session) : XPObject(session)
{

    Product product;
    string code;

    [Size(255)]
    [RuleRequiredField]
    public string Code
    {
        get => code;
        set => SetPropertyValue(nameof(Code), ref code, value);
    }

    [Association("ProductSection-ProductSectionAlgorithms")]
    public XPCollection<ProductSectionAlgorithm> ProductSectionAlgorithms
    {
        get
        {
            return GetCollection<ProductSectionAlgorithm>(nameof(ProductSectionAlgorithms));
        }
    }


    [Association("Product-Sections")]
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