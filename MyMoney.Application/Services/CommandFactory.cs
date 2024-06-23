using MyMoney.Application.Services.CreateTransaction.Commands;

namespace MyMoney.Application.Services;

public static class CommandFactory
{
    public static CreateTransactionCommand CreateTransactionCommand(
        Guid userId,
        string accountName,
        string category,
        int amount,
        DateTime dateTime)
    {
        return new CreateTransactionCommand(userId, accountName, category, amount, dateTime);
    }
}

