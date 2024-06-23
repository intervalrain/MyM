using MediatR;

using Microsoft.AspNetCore.Mvc;

using MyMoney.Application.Common.Interfaces;
using MyMoney.Application.Common.Security.User;
using MyMoney.Application.Services;
using MyMoney.Contracts.Transactions;
using MyMoney.Domain;

namespace MyMoney.Api.Controllers;

public class TransactionsController : ApiController
{
    private readonly IMediator _mediator;
    private readonly CurrentUser _currentUser;

    public TransactionsController(IMediator mediator, ICurrentUserProvider currentUserProvider)
    {
        _mediator = mediator;
        _currentUser = currentUserProvider.CurrentUser;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> FetchAllTransactions(CancellationToken cancellationToken)
    {
        var query = QueryFactory.FetchAllTransactionsQuery(_currentUser.Id);

        var result = await _mediator.Send(query, cancellationToken);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken)
    {
        var command = CommandFactory.CreateTransactionCommand(
            _currentUser.Id,
            request.AccountName,
            request.Category,
            request.Amount,
            request.DateTime);

        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(Ok, Problem);
    }
}