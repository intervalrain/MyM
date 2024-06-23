using MyMoney.Domain.Common;

namespace MyMoney.Domain.Events;

public record AccountSetEvent(Account Account) : IDomainEvent;