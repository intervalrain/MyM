using MyMoney.Application.Tokens.Queries.Generate;
using MyMoney.Application.Transactions.FetchAllTransactions.Queries;

namespace MyMoney.Application;

public static class QueryFactory
{
    public static GenerateTokenQuery GenerateTokenQuery(Guid? userId, string firstName, string lastName, string email)
    {
        return new GenerateTokenQuery(userId, firstName, lastName, email);
    }

    public static FetchAllTransactionsQuery FetchAllTransactionsQuery(Guid userId)
    {
        return new FetchAllTransactionsQuery(userId);
    }

    public static GetTransactionQuery GetTransactionQuery(Guid userId, Guid transactionId)
    {
        return new GetTransactionQuery(userId, transactionId);
    }
}

