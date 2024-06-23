using MyMoney.Application;
using MyMoney.Application.Transactions.FetchAllTransactions.Queries;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Transactions;

public static class TransactionQueryFactory
{
    public static FetchAllTransactionsQuery CreateFetchAllTransactionsQuery(Guid? userId = null)
    {
        return QueryFactory.FetchAllTransactionsQuery(userId ?? Constants.User.UserId);
    }
}

