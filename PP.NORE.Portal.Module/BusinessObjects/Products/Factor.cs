using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace PP.NORE.Portal.Module.BusinessObjects.Products;

[DefaultClassOptions]
public class Factor(Session session) : XPObject(session)
{

    string status;
    string validationMessage;
    Step steps;
    int sequence;
    string algorithm;
    string code;

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


    public int Sequence
    {
        get => sequence;
        set => SetPropertyValue(nameof(Sequence), ref sequence, value);
    }


    [Size(500)]
    public string ValidationMessage
    {
        get => validationMessage;
        set => SetPropertyValue(nameof(ValidationMessage), ref validationMessage, value);
    }

    
    [Size(50)]
    public string Status
    {
        get => status;
        set => SetPropertyValue(nameof(Status), ref status, value);
    }

    [Association("Step-Factors")]
	[VisibleInDashboards(false)]
	[VisibleInDetailView(false)]
	[VisibleInListView(false)]
	[VisibleInLookupListView(false)]
	public Step Steps
	{
		get => steps;
		set => SetPropertyValue(nameof(Steps), ref steps, value);
	}


}