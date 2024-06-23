using Microsoft.EntityFrameworkCore;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain;
using MyMoney.Infrastructure.Common.Persistence;

namespace MyMoney.Infrastructure.Persistence;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task AddTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await AddAsync(transaction, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> FetchAllTransactionAsync(CancellationToken cancellationToken)
    {
        return await FetchAllAsync(cancellationToken);
    }

    public async Task<Transaction?> GetTransactionAsync(Guid id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}

