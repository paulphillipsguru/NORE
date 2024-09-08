namespace PP.NORE.Shared.Models
{
	public record ErrorModel
	{
		public required int Id { get; set; }
		public required bool IsBase { get; set; }
		public required string ErrorType { get; set; }
		public required string Message { get; set; }


	}
}
