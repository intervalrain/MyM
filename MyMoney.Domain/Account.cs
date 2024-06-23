using ErrorOr;

using MyMoney.Domain.Common;
using MyMoney.Domain.Events;

namespace MyMoney.Domain;

public class Account : Entity
{
    public Guid UserId { get; init; }
    public string AccountName { get; init; }
    public int InitialAmount { get; init; }
    private readonly List<Guid> _transactionIds = new();

    public Account(Guid userId, string name, int initialAmount)
        : base(Guid.NewGuid())
    {
        UserId = userId;
        AccountName = name;
        InitialAmount = initialAmount;
    }

    public ErrorOr<Success> AddTransaction(Transaction transaction)
    {
        _transactionIds.Add(transaction.Id);
        _domainEvents.Add(new TransactionSetEvent(transaction));

        return Result.Success;
    }

    private Account()
    {
    }
}

