using MyMoney.Application.Common.Security.User;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Security;

public static class CurrentUserFactory
{
    public static CurrentUser CreateCurrentUser(
        Guid? id = null,
        string? firstName = null,
        string? lastName = null,
        string? email = null,
        List<string> permissions = null!,
        List<string> roles = null!)
    {
        var permissionPervider = new TestPermissionProvider();

        return new CurrentUser(
            Id: id ?? Constants.User.UserId,
            FirstName: firstName ?? Constants.User.FirstName,
            LastName: lastName ?? Constants.User.LastName,
            Email: email ?? Constants.User.Email,
            Permissions: permissions ?? Constants.User.Permissions,
            Roles: roles ?? Constants.User.Roles);
    }
}

