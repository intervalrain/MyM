using MyMoney.Domain;

namespace MyMoney.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken);
    Task AddUserAsync(User user, CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellation);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}