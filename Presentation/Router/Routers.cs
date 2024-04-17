namespace Presentation.Router;

internal static class Routers
{
    #region Const Params
    public const string root = "api";
    public const string version = "v1";
    public const string Rule = root + "/" + version + "/";
    public const string SingleRoute = "/{id:Guid}";
    #endregion

    #region Order
    public static class Order
    {
        public const string Prefix = Rule + "Order/";

        public const string Create = $"{Prefix}Create";
        public const string Update = $"{Prefix}Update";
    }
    #endregion

    #region Product
    public static class Product
    {
        public const string Prefix = Rule + "Product/";

        public const string Create = $"{Prefix}Create";
        public const string Update = $"{Prefix}Update";
    }
    #endregion

    #region User
    public static class User
    {
        public const string Prefix = Rule + "User/";

        public const string Create = $"{Prefix}Create";
        public const string Update = $"{Prefix}Update";
        public const string GetUserById = $"{Prefix}Update{SingleRoute}";
        public const string DeleteById = $"{Prefix}Delete{SingleRoute}";
        public const string Login = $"{Prefix}Login";
        public const string Register = $"{Prefix}Register";
    }
    #endregion
}