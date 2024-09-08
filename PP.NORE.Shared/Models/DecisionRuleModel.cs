
namespace PP.NORE.Shared.Models;

public record DecisionRuleModel
{
    public int Id { get; set; } = 0;
    public bool IsBase { get; set; } = false;
    public DecisionType RuleType { get; set; } = DecisionType.Applicability;
    public RatingStatus OutComeStatus { get; set; } = RatingStatus.NotRated;
    public required string Code { get; set; }
    public required string Description { get; set; }
    public string Message {  get; set; } = string.Empty;
    public required string Algorithm { get; set; }
}
