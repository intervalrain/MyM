using MyMoney.Application.Services;
using MyMoney.Application.Services.FetchAllTransactions.Queries;
using MyMoney.TestCommon.TestConstants;

namespace MyMoney.TestCommon.Transactions;

public static class TransactionQueryFactory
{
    public static FetchAllTransactionsQuery CreateFetchAllTransactionsQuery(Guid? userId = null)
    {
        return QueryFactory.FetchAllTransactionsQuery(userId ?? Constants.User.UserId);
    }
}

