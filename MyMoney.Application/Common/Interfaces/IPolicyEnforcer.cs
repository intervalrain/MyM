using ErrorOr;

using MyMoney.Application.Common.Security.Request;
using MyMoney.Application.Common.Security.User;

namespace MyMoney.Application.Common.Interfaces;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizeableRequest<T> request,
        CurrentUser currentUser,
        string policy);
}