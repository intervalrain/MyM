namespace MyMoney.Contracts.Accounts;

public record AccountResponse(Guid UserId, Guid AccountId, string AccountName, int InitialAmount);