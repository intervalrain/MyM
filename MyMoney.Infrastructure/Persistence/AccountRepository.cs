using Microsoft.EntityFrameworkCore;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain;
using MyMoney.Infrastructure.Common.Persistence;

namespace MyMoney.Infrastructure.Persistence;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<Account?> GetAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .Where(a => a.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAccountAsync(Account account, CancellationToken cancellationToken)
    {
        await UpdateAsync(account, cancellationToken);
    }
}

