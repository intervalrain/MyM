using ErrorOr;

using MediatR;

using MyMoney.Application.Dtos;

namespace MyMoney.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email) : IRequest<ErrorOr<UserDto>>;
