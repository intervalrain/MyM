namespace MyMoney.Contracts.Accounts;

public record CreateAccountRequest(Guid UserId, string AccountName, int InitialAmount);