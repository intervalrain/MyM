using FluentAssertions;

using MyMoney.Application.Dtos;
using MyMoney.Application.Services.CreateTransaction.Commands;

namespace MyMoney.TestCommon.Transactions;

public static class TransactionValidator
{
    public static void AssertCreatedFrom(this TransactionDto transaction, CreateTransactionCommand command)
    {
        transaction.UserId.Should().Be(command.UserId);
        transaction.DateTime.Should().Be(command.DateTime);
        transaction.Category.Should().Be(command.Category);
        transaction.Amount.Should().Be(command.Amount);
    }
}

