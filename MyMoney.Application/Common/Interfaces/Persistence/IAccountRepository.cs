using MyMoney.Domain;

namespace MyMoney.Application.Common.Interfaces.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task UpdateAccountAsync(Account account, CancellationToken cancellationToken);
    Task AddAccountAsync(Account account, CancellationToken cancellationToken);
}