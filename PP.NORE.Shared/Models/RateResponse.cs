namespace PP.NORE.Shared.Models
{
    public record RateResponse
    {

        public Guid RateId { get; set; } = Guid.NewGuid();
        public RatingStatus Status { get; set; } = RatingStatus.NotRated;
        public Dictionary<string, string> Decisions{ get; set; } = [];        
        public Dictionary<string, dynamic> LineItems { get; set; } = [];
        public string Message { get; set; } = string.Empty;
        public DateTime DateOfRate { get; set; } = DateTime.Now;
        public double TotalPremium { get; set; } = 0;
        public List<RateInfo> Debug { get; set; } = []; // should not be used in PROD!
    }
}
