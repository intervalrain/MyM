using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using MyMoney.Application.Common.Interfaces.Persistence;
using MyMoney.Domain.Common;

namespace MyMoney.Infrastructure.Common.Persistence;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext _context;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAllAsync(CancellationToken cancellationToken)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _entities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _entities.Where(predicate).ToListAsync(cancellationToken);
    }


    public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Update(entity);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _entities.Remove(entity);
        return _context.SaveChangesAsync(cancellationToken);
    }
}

