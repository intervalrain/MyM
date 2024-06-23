namespace MyMoney.Application.Common.Interfaces;

public interface IPermissionProvider
{
    IEnumerable<string> GetAllPermissions();

    IEnumerable<string> GetPermissionsByCategory(string category);
}

