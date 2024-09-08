using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace PP.NORE.Portal.Module.BusinessObjects.ProductSections;

[DefaultClassOptions]
public class ProductSectionAlgorithm(Session session) : XPObject(session)
{

    ProductSection productSection;
   
    string code;

    [Size(255)]
    [RuleRequiredField]
    public string Code
    {
        get => code;
        set => SetPropertyValue(nameof(Code), ref code, value);
    }


    [Association("ProductSectionAlgorithm-ProductSectionAlgorithmStep")]
    public XPCollection<ProductSectionAlgorithmStep> ProductSectionAlgorithmStep
    {
        get
        {
            return GetCollection<ProductSectionAlgorithmStep>(nameof(ProductSectionAlgorithmStep));
        }
    }

    [Association("ProductSection-ProductSectionAlgorithms")]
    [VisibleInDashboards(false)]
    [VisibleInDetailView(false)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    public ProductSection ProductSection
    {
        get => productSection;
        set => SetPropertyValue(nameof(ProductSection), ref productSection, value);
    }

}