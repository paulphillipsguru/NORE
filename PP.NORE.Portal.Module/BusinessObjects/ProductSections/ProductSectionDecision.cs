using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PP.NORE.Portal.Module.BusinessObjects.Base;

namespace PP.NORE.Portal.Module.BusinessObjects.ProductSections;

[DefaultClassOptions]
public class ProductSectionDecision(Session session) : DecisionRule(session)
{
    
}