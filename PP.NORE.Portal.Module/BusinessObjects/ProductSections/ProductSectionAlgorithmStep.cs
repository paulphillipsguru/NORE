using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace PP.NORE.Portal.Module.BusinessObjects.ProductSections;

[DefaultClassOptions]
public class ProductSectionAlgorithmStep(Session session) : XPObject(session)
{

    ProductSectionAlgorithm productSectionAlgorithm;
    string code;
    string algorithm;

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
    [Association("ProductSectionAlgorithm-ProductSectionAlgorithmStep")]
    [VisibleInDashboards(false)]
    [VisibleInDetailView(false)]
    [VisibleInListView(false)]
    [VisibleInLookupListView(false)]
    public ProductSectionAlgorithm ProductSectionAlgorithm
    {
        get => productSectionAlgorithm;
        set => SetPropertyValue(nameof(ProductSectionAlgorithm), ref productSectionAlgorithm, value);
    }
}