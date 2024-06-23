using ErrorOr;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.User;

namespace MyMoney.Infrastructure.Security;

public class AuthorizationService : IAuthorizationService
{
    private readonly IPolicyEnforcer _policyEnforcer;
    private readonly CurrentUser _currentUser;

    public AuthorizationService(IPolicyEnforcer policyEnforcer, ICurrentUserProvider currentUserProvider)
    {
        _policyEnforcer = policyEnforcer;
        _currentUser = currentUserProvider.CurrentUser;
    }

    public ErrorOr<Success> AuthorizeCurrentUser<T>(IAuthorizeableRequest<T> request, List<string> requiredRoles, List<string> requiredPermissions, List<string> requiredPolicies)
    {
        if (requiredRoles.Except(_currentUser.Roles).Any())
        {
            return Error.Unauthorized(description: "User is missing required roles for taking this action.");
        }

        if (requiredPermissions.Except(_currentUser.Permissions).Any())
        {
            return Error.Unauthorized(description: "User is missing required permissions for taking this action.");
        }

        foreach (var policy in requiredPolicies)
        {
            var authorizationAgainstPolicyResult = _policyEnforcer.Authorize(request, _currentUser, policy);

            if (authorizationAgainstPolicyResult.IsError)
            {
                return authorizationAgainstPolicyResult.Errors;
            }
        }

        return Result.Success;
    }
}
