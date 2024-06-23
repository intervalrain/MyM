namespace MyMoney.Contracts.Users;

public record UserResponse(
    Guid UserID,
    string FirstName,
    string LastName,
    string Email,
    int UserType);