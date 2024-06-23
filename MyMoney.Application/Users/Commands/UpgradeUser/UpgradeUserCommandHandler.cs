using ErrorOr;

using MediatR;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Application.Dtos;
using MyMoney.Domain.Enums;

namespace MyMoney.Application.Users.Commands.UpgradeUser;

public class UpgradeUserCommandHandler : IRequestHandler<UpgradeUserCommand, ErrorOr<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public UpgradeUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserDto>> Handle(UpgradeUserCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.UserType.ToString(), out UserType userType))
        {
            return Error.Forbidden(description: "Argument of UserType is out of range(0-3)");
        }

        var user = await _userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: $"User {request.UserId} not found");
        }

        if (userType == user.UserType)
        {
            return Error.Conflict(description: $"User has already been {user.UserType}");
        }

        user.UserType = userType;

        await _userRepository.UpdateUserAsync(user, cancellationToken);

        return UserDto.ToDto(user);
    }
}

