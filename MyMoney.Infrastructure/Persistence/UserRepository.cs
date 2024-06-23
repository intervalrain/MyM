using Microsoft.EntityFrameworkCore;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain;
using MyMoney.Infrastructure.Common.Persistence;

namespace MyMoney.Infrastructure.Persistence;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellation)
    {
        await UpdateAsync(user, cancellation);
    }
}

