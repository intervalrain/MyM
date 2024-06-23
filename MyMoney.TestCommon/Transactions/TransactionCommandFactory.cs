using MyMoney.Application.Services;
using MyMoney.Application.Services.CreateTransaction.Commands;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Transactions;

public static class TransactionCommandFactory
{
    public static CreateTransactionCommand CreateCreateTransactionCommand(
        Guid? userId = null,
        string? accountName = null,
        string? category = null,
        int? amount = null,
        DateTime? dateTime = null)
    {
        return CommandFactory.CreateTransactionCommand(
            userId: userId ?? Constants.User.UserId,
            accountName: accountName ?? Constants.Account.AccountName,
            category: category ?? Constants.Transaction.Category,
            amount: amount ?? Constants.Transaction.Amount,
            dateTime: dateTime ?? Constants.Transaction.DateTime);
    }
}

