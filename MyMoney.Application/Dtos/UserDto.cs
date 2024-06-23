using MyMoney.Domain;

namespace MyMoney.Application.Dtos;

public record UserDto(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    int UserType)
{
    public static UserDto ToDto(User user)
    {
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            (int)user.UserType);
    }
}