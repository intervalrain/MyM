using MyMoney.Domain;

namespace MyMoney.Application.Common.Interfaces.Persistence;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> FetchAllTransactionAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);
    Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<Transaction?> GetTransactionAsync(Guid id, CancellationToken cancellationToken);
    Task AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
}