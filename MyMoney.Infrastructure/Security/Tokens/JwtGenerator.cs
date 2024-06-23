using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Domain;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMoney.Infrastructure.Security.Tokens;

public class UserNotFoundException : Exception
{
}

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUserRepository _userRepository;
    private readonly IPermissionProvider _permissionProvider;
    private readonly IRoleProvider _roleProvider;

    public JwtGenerator(IOptions<JwtSettings> jwtOptions, IUserRepository userRepository, IPermissionProvider permissionProvider, IRoleProvider roleProvider)
    {
        _jwtSettings = jwtOptions.Value;
        _userRepository = userRepository;
        _permissionProvider = permissionProvider;
        _roleProvider = roleProvider;
    }

    public async Task<string> GenerateToken(
        Guid id,
        string firstName,
        string lastName,
        string email)
    {
        var user = await _userRepository.GetUserAsync(id, default) ?? throw new UserNotFoundException();

        if (user.FirstName != firstName ||
            user.LastName != lastName ||
            user.Email != email)
        {
            throw new UnauthorizedAccessException();
        }

        var permissions = _permissionProvider.GetPermissions(user.UserType).ToList();
        var roles = _roleProvider.GetRoles(id).ToList();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, firstName),
            new(JwtRegisteredClaimNames.FamilyName, lastName),
            new(JwtRegisteredClaimNames.Email, email),
            new("id", id.ToString()),
        };

        roles.ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
        permissions.ForEach(permission => claims.Add(new(nameof(Permission), permission)));

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}