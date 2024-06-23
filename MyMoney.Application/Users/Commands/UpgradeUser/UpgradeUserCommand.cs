using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Application.Common.Security.Roles;
using MyMoney.Application.Dtos;

namespace MyMoney.Application.Users.Commands.UpgradeUser;

[Authorize(Permissions = Permission.User.Update, Roles = Role.Admin)]
public record UpgradeUserCommand(Guid UserId, int UserType) : IAuthorizeableRequest<ErrorOr<UserDto>>;