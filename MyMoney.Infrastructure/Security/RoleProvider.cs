using System.Text.Json;

using Microsoft.Extensions.Configuration;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.Roles;

namespace MyMoney.Infrastructure.Security;

public class RoleNotDefineException : Exception { }

public class RoleProvider : IRoleProvider
{
    private readonly Guid AdminId;

    public RoleProvider(IConfiguration configuration)
    {
        var id = configuration.GetSection(Role.Admin).Get<string>() ?? throw new RoleNotDefineException();
        AdminId = Guid.Parse(id);
    }

    public IEnumerable<string> GetRoles(Guid guid)
    {
        return AdminId == guid
            ? new string[] { Role.Admin, Role.User }
            : new string[] { Role.User };
    }
}

