using MyMoney.Application.Common.Interfaces;
using MyMoney.Infrastructure.Security;

namespace MyMoney.TestCommon.Security;

public class TestPermissionProvider : IPermissionProvider
{
    private static readonly IPermissionProvider _permissionProvider = new PermissionProvider();

    public IEnumerable<string> GetAllPermissions()
    {
        return _permissionProvider.GetAllPermissions();
    }

    public IEnumerable<string> GetPermissionsByCategory(string category)
    {
        return _permissionProvider.GetPermissionsByCategory(category);
    }
}