namespace MyMoney.Contracts.Transactions;

public record TransactionResponse(
    Guid UserId,
    Guid AccountId,
    Guid TransactionId,
    string Category,
    string Amount,
    DateTime DateTime);