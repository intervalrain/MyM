using MyMoney.Domain.Common;

namespace MyMoney.Domain.Events;

public record TransactionSetEvent(Transaction Transaction) : IDomainEvent;