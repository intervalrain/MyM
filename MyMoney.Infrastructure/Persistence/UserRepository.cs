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

    public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken: cancellationToken);
    }
}

