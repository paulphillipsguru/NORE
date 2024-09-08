using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Base;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;

[DefaultClassOptions]
public class ProductDecision(Session session) : DecisionRule(session)
{

    Product product;
    [Association("Product-Decisions")]
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