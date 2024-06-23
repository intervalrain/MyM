using Microsoft.AspNetCore.Http;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.User;
using MyMoney.Application.Common.Security.Permissions;

using System.Security.Claims;

using Throw;
using System.IdentityModel.Tokens.Jwt;

namespace MyMoney.Infrastructure.Security;


public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly CurrentUser _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser CurrentUser => _currentUser;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        httpContextAccessor.HttpContext.ThrowIfNull();

        var id = Guid.Parse(GetSingleClaimValue("id"));
        var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        var lastName = GetSingleClaimValue(ClaimTypes.Surname);
        var email = GetSingleClaimValue(ClaimTypes.Email);
        var permissions = GetClaimValues(nameof(Permission));
        var roles = GetClaimValues(ClaimTypes.Role);

        _currentUser = new CurrentUser(id, firstName, lastName, email, permissions, roles);
    }

    private List<string> GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}
