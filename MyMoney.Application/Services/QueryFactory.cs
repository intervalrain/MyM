using MyMoney.Application.Services.FetchAllTransactions.Queries;

namespace MyMoney.Application.Services;

public static class QueryFactory
{
    public static FetchAllTransactionsQuery FetchAllTransactionsQuery(Guid userId)
    {
        return new FetchAllTransactionsQuery(userId);
    }

    public static GetTransactionQuery GetTransactionQuery(Guid userId, Guid transactionId)
    {
        return new GetTransactionQuery(userId, transactionId);
    }
}

