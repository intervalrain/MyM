using MediatR;

using Microsoft.AspNetCore.Mvc;
using MyMoney.Application;
using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.User;
using MyMoney.Application.Dtos;
using MyMoney.Contracts.Accounts;

namespace MyMoney.Api.Controllers;

public class AccountsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly CurrentUser _currentUser;

    public AccountsController(IMediator mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
        _currentUser = currentUserProvider.CurrentUser;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var command = CommandFactory.CreateAccountCommand(
            request.UserId,
            request.AccountName,
            request.InitialAmount);

        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            accountDto => Ok(ToDto(accountDto)),
            Problem);
    }

    private static AccountResponse ToDto(AccountDto account)
    {
        return new AccountResponse(
            account.UserId,
            account.AccountId,
            account.AccountName,
            account.InitialAmount);
    }
}

