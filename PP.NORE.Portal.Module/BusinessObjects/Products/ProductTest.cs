using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Shared.Models;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;

[DefaultClassOptions]
[ImageName("ModelEditor_ModelMerge")]
public class ProductTest(Session session) : XPObject(session)
{

    string message;
    Product product;
    bool passed;
    double actualPremium;
    RatingStatus actualStatus;
    double expectedPremium;
    RatingStatus expectedStatus;
    string testPayLoad;

    [Size(SizeAttribute.Unlimited)]
    [RuleRequiredField]
    public string TestPayLoad
    {
        get => testPayLoad;
        set => SetPropertyValue(nameof(TestPayLoad), ref testPayLoad, value);
    }


    [Size(255)]    
    public RatingStatus ExpectedStatus
    {
        get => expectedStatus;
        set => SetPropertyValue(nameof(ExpectedStatus), ref expectedStatus, value);
    }


    public double ExpectedPremium
    {
        get => expectedPremium;
        set => SetPropertyValue(nameof(ExpectedPremium), ref expectedPremium, value);
    }


    [Size(255)]
    public RatingStatus ActualStatus
    {
        get => actualStatus;
        set => SetPropertyValue(nameof(ActualStatus), ref actualStatus, value);
    }


    public double ActualPremium
    {
        get => actualPremium;
        set => SetPropertyValue(nameof(ActualPremium), ref actualPremium, value);
    }

    
    [Size(500)]
    public string Message
    {
        get => message;
        set => SetPropertyValue(nameof(Message), ref message, value);
    }


    public bool Passed
    {
        get => passed;
        set => SetPropertyValue(nameof(Passed), ref passed, value);
    }

    [Association("Product-ProductTests")]
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