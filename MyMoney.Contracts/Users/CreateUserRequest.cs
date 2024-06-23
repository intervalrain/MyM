namespace MyMoney.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email);