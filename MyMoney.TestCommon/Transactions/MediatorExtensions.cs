using ErrorOr;

using FluentAssertions;

using MediatR;

using MyMoney.Application;
using MyMoney.Application.Dtos;
using MyMoney.Application.Transactions.CreateTransaction.Commands;
using MyMoney.Application.Transactions.FetchAllTransactions.Queries;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Transactions;

public static class MediatorExtensions
{
    public static async Task<TransactionDto> CreateTransactionAsync(this IMediator mediator, CreateTransactionCommand? command = null)
    {
        command ??= CommandFactory.CreateTransactionCommand(
            userId: Constants.User.UserId,
            accountName: Constants.Account.AccountName,
            category: Constants.Transaction.Category,
            amount: Constants.Transaction.Amount,
            dateTime: Constants.Transaction.DateTime);

        var result = await mediator.Send(command);

        result.IsError.Should().BeFalse();
        result.Value.AssertCreatedFrom(command);

        return result.Value;
    }

    public static async Task<ErrorOr<List<TransactionDto>>> FetchAllTransactionsAsync(this IMediator mediator, FetchAllTransactionsQuery? query = null)
    {
        query ??= QueryFactory.FetchAllTransactionsQuery(Constants.User.UserId);

        return await mediator.Send(query);
    }

    public static async Task<ErrorOr<TransactionDto>> GetTransactionAsync(this IMediator mediator, GetTransactionQuery? query = null)
    {
        query ??= QueryFactory.GetTransactionQuery(Constants.User.UserId, Constants.Transaction.TransactionId);

        return await mediator.Send(query);
    }
}