using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyMoney.Application;
using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.User;
using MyMoney.Application.Dtos;
using MyMoney.Contracts.Users;

namespace MyMoney.Api.Controllers;

public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly CurrentUser _currentUser;

    public UsersController(IMediator mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
        _currentUser = currentUserProvider.CurrentUser;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = CommandFactory.CreateUserCommand(
            request.FirstName,
            request.LastName,
            request.Email);

        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            userDto => Ok(ToDto(userDto)),
            Problem);
    }

    [HttpPut]
    public async Task<IActionResult> UpgradeUser([FromBody] UpgradeUserRequest request, CancellationToken cancellationToken)
    {
        var command = CommandFactory.UpgradeUserCommand(request.UserId, request.UserType);

        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            userDto => Ok(ToDto(userDto)),
            Problem);
    }

    private static UserResponse ToDto(UserDto user)
    {
        return new UserResponse(user.UserId, user.FirstName, user.LastName, user.Email, user.UserType);
    }
}