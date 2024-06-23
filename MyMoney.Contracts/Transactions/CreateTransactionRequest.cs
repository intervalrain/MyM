namespace MyMoney.Contracts.Transactions;

public record CreateTransactionRequest(
    string AccountName,
    string Category,
    int Amount,
    DateTime DateTime);