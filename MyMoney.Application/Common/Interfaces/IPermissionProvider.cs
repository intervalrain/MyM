using MyMoney.Domain.Enums;

namespace MyMoney.Application.Common.Interfaces;

public interface IPermissionProvider
{
    IEnumerable<string> GetPermissions(UserType userType);
}