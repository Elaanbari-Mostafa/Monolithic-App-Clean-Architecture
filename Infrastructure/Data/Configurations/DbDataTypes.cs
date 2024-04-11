namespace Infrastructure.Data.Configurations;

internal static class DbDataTypes
{
    public const string Date = "date";
    public const string Int = "int";
    public static Func<int, string> Nvarchar = number => $"nvarchar({number})";
    public static Func<int, int, string> Decimal = (n, v) => $"decimal({n},{v})";
}