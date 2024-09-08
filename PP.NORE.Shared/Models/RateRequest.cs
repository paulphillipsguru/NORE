using System.Text.Json;

namespace PP.NORE.Shared.Models
{
    public record RateRequest
    {
        public required string Entity { get; set; }
        public required string InstanceName { get; set; }
        public required string ProductCode { get; set; }
        public string ProductDataPath { get; set; } = string.Empty;
        public MemoryStream ProductBinary { get; set; } = new();
        public required Dictionary<string, JsonElement> Answers { get; set; } = [];
        public List<RateInfo> Debug { get; set; } = []; // should not be used in PROD!
        public double TotalPremium { get; set; } = 0;
        public override string ToString()
        {
            return Entity + InstanceName + " " + ProductCode;
        }

        public bool IsTrue(string question)
        {
            return (Answers.ContainsKey(question) && Answers[question].GetBoolean());
        }

        public bool IsFalse(string question)
        {
            return (Answers.ContainsKey(question) && !Answers[question].GetBoolean());
        }


        public string GetString(string question) { 
            if (!Answers.ContainsKey(question)) return "";
            return Answers[question].GetString();
        }

        public double GetDouble(string question)
        {
            if (!Answers.ContainsKey(question)) return 0;
            return Answers[question].GetDouble();
        }

        public decimal GetDecimal(string question)
        {
            if (!Answers.ContainsKey(question)) return 0;
            return Answers[question].GetDecimal();
        }

        public string GetText(string question)
        {
            if (!Answers.ContainsKey(question)) return "";
            return Answers[question].GetRawText();
        }
    }
}
