using MyMoney.Application.Accounts.Commands;
using MyMoney.Application.Transactions.CreateTransaction.Commands;
using MyMoney.Application.Users.Commands.CreateUser;
using MyMoney.Application.Users.Commands.UpgradeUser;

namespace MyMoney.Application;

public static class CommandFactory
{
    public static CreateUserCommand CreateUserCommand(string firstName, string lastName, string email)
    {
        return new CreateUserCommand(firstName, lastName, email);
    }

    public static UpgradeUserCommand UpgradeUserCommand(Guid userId, int userType)
    {
        return new UpgradeUserCommand(userId, userType);
    }

    public static CreateAccountCommand CreateAccountCommand(Guid userId, string accountName, int initialAmount)
    {
        return new CreateAccountCommand(userId, accountName, initialAmount);
    }

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

