namespace PP.NORE.Shared.Models
{
    public record CodeModule
    {
        public required string Code { get; set; }
        public required string Template { get; set; }
        public bool IsData { get; set; } = false;
    }
}
