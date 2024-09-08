namespace PP.NORE.Shared.Models
{
    public record RateStep
    {
		public int Id { get; set; } = 0;
        public bool IsBase { get; set; } = false;
		public required string Code { get; set; }
        public List<RateFactor> Factors { get; set; } = [];
        public required string Algorithm { get; set; }
    }


    public class RateFactor
    {
        public int Id { get; set; } = 0;
        public bool IsBase { get; set; } = false;
        public string Code { get; set; }
        public string Algorithm { get; set; }
        public RateFactor(string code, string algorithm)
        {
            Code = code;
            Algorithm = algorithm;
        }
    }
}
