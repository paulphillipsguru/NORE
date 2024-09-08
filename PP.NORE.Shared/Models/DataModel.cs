namespace PP.NORE.Shared.Models
{
    public class DataModel
    {
        public string TableName { get; set; } = string.Empty;
        public List<Dictionary<string, object>> Data { get; set; } = [];
        public List<Dictionary<string, object>> ColumnInfo { get; set; } = [];
    }
}
