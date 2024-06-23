namespace MyMoney.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Transaction
    {
        public const string Create = "set:transaction";
        public const string Get = "get:transaction";
        public const string FetchAll = "fetchall:transaction";
        public const string Delete = "delete:transaction";
        public const string Update = "update:transaction";
    }
}