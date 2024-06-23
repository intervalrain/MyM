using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.User;

namespace MyMoney.TestCommon.Security;

public class TestCurrentUserProvider : ICurrentUserProvider
{
    private CurrentUser? _currentUser;

    public void Returns(CurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    public CurrentUser CurrentUser => _currentUser ?? CurrentUserFactory.CreateCurrentUser();
}

