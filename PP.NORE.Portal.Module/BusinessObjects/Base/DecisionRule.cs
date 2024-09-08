using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PP.NORE.Shared.Models;

namespace PP.NORE.Portal.Module.BusinessObjects.Base;

[DefaultClassOptions]
public abstract class DecisionRule(Session session) : XPObject(session)
{

    RatingStatus outComeStatus;
    string status;
    string validationMessage;
    string message;
    DecisionType decisionType;
    string algorithm;
    string code;

    [Size(255)]
    [RuleUniqueValue]
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

    public DecisionType DecisionType
    {
        get => decisionType;
        set => SetPropertyValue(nameof(DecisionType), ref decisionType, value);
    }


    [Size(2000)]
    [RuleRequiredField]
    public string Message
    {
        get => message;
        set => SetPropertyValue(nameof(Message), ref message, value);
    }


    
    public RatingStatus OutComeStatus
    {
        get => outComeStatus;
        set => SetPropertyValue(nameof(OutComeStatus), ref outComeStatus, value);
    }

    [Size(2000)]    
    [VisibleInListView(true)]
    
    public string ValidationMessage
    {
        get => validationMessage;
        set => SetPropertyValue(nameof(ValidationMessage), ref validationMessage, value);
    }

    
    [Size(50)]
    [VisibleInListView(true)]
    public string Status
    {
        get => status;
        set => SetPropertyValue(nameof(Status), ref status, value);
    }

}