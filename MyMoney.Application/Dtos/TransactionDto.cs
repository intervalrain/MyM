using MyMoney.Domain;

namespace MyMoney.Application.Dtos;

public record TransactionDto(Guid Id, Guid UserId, Guid AccountId, string Category, int Amount, DateTime DateTime)
{
    public static TransactionDto ToDto(Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.UserId,
            transaction.AccountId,
            transaction.Category,
            transaction.Amount,
            transaction.DateTime);
    }
}