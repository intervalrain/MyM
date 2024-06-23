using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.Permissions;
using MyMoney.Application.Dtos;
using MyMoney.Application.Common.Security.Policies;

namespace MyMoney.Application.Accounts.Commands;

[Authorize(Permissions = Permission.Account.Create, Policies = Policy.SelfOrAdmin)]
public record CreateAccountCommand(Guid UserId, string AccountName, int InitAmount) : IAuthorizeableRequest<ErrorOr<AccountDto>>;