using ErrorOr;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.User;

namespace MyMoney.Infrastructure.Security;

public class PolicyEnforcer : IPolicyEnforcer
{
    public const string SelfOrAdmin = nameof(SelfOrAdmin);
    public const string Admin = nameof(Admin);

    public ErrorOr<Success> Authorize<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser, string policy)
    {
        return policy switch
        {
            SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
            _ => Error.Unexpected(description: "Unknown policy name")
        };
    }

    private static ErrorOr<Success> SelfOrAdminPolicy<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser)
    {
        return request.UserId == currentUser.Id || currentUser.Roles.Contains(Admin)
            ? Result.Success
            : Error.Unauthorized(description: "Requesting user failed policy requirement");
    }
}