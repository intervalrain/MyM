using MyMoney.Domain;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Transactions;

public static class TransactionFactory
{
    public static Transaction CreateTransaction(
        Guid? userId = null,
        Guid? accountId = null,
        string? category = null,
        int? amount = null,
        DateTime? dateTime = null)
    {
        return new Transaction(
            userId: userId ?? Constants.User.UserId,
            accountId: accountId ?? Constants.Account.AccountId,
            category: category  ?? Constants.Transaction.Category,
            amount: amount ?? Constants.Transaction.Amount,
            dateTime: dateTime ?? Constants.Transaction.DateTime);
    }
}

