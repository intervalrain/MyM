using MyMoney.Application.Common.Security.User;

namespace MyMoney.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser CurrentUser { get; }
}

