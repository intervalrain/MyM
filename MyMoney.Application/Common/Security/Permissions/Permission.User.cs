namespace MyMoney.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class User
    {
        public const string Get = "get:account";
        public const string FetchAll = "fetchall:account";
        public const string Delete = "delete:account";
        public const string Update = "update:account";
    }
}

