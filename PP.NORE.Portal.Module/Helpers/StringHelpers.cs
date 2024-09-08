namespace PP.NORE.Portal.Module.Helpers
{
    public static class StringHelpers
    {
        public static string Clean(this string value)
        {
            return value.Replace("\"", "");
        }
    }
}
