namespace PP.NORE.Builder.Models
{
    public class ReportModel
    {
        public Dictionary<string, string> AvailabilityRules { get; set; } = [];
        public Dictionary<string, string> DeclineRules { get; set; } = [];
        public List<ReportStepModel> Steps { get; set; } = [];
        
    }

    public class ReportStepModel
    {
        public string Code { get; set; } = string.Empty;
        public string Algorithm { get; set; } = string.Empty;
        public Dictionary<string, string> Factors { get; set; } = [];

    }
}
