using MyMoney.Domain;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Users;

public static class UserFactory
{
    public static User CreateUser(
        Guid? id = null,
        string? firstName = null,
        string? lastName = null,
        string? email = null)
    {
        return new User(
            id ?? Constants.User.UserId,
            firstName ?? Constants.User.FirstName,
            lastName ?? Constants.User.LastName,
            email ?? Constants.User.Email);
    }
}