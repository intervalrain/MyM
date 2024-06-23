using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;
using MyMoney.Domain;

namespace MyMoney.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (user != null)
        {
            Error.Conflict(description: "The email has already been registered");
        }

        var id = Guid.NewGuid();
        user = new User(id, request.FirstName, request.LastName, request.Email);

        await _userRepository.AddUserAsync(user, cancellationToken);

        return UserDto.ToDto(user);
    }
}

