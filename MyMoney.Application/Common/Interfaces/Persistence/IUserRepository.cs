using MyMoney.Domain;

namespace MyMoney.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken);
}