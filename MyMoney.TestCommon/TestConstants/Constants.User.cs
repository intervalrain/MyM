using MyMoney.Application.Common.Security.Roles;
using MyMoney.TestCommon.Security;

namespace MyMoney.TestCommon.TestConstants;

public static partial class Constants
{
    public static class User
    {
        public static readonly Guid UserId = Guid.NewGuid();
        public const string FirstName = "Rain";
        public const string LastName = "Hu";
        public const string Email = "intervalrain@gmail.com";
        public static IReadOnlyList<string> Permissions = new TestPermissionProvider().GetAllPermissions().ToList().AsReadOnly();
        public static IReadOnlyList<string> Roles = new List<string> { nameof(Role.Admin), nameof(Role.User) };
    }
}