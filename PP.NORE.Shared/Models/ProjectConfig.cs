namespace PP.NORE.Shared.Models
{
    public class ProjectConfig
    {
        public required string EntityName { get; set; } 
        public required string InstanceName { get; set; } 
        public required string ProductName { get; set; } 
        public List<string> RequiredLookups { get; set; } = []; 
        public MemoryStream DataModel { get; set; } = new();
        
        public List<CodeModule> CodeTemplate { get; set; } = [];
        public List<DecisionRuleModel> DecisionRules { get; set; } = []; 
        
        public List<RateStep> Steps{ get; set; } = []; 
        public string ProductDataPath {  get; set; } = string.Empty;    

    }
}
