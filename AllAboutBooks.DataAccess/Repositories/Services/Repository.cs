using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _applicationDbContext;
    internal DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<TEntity>();
    }

    public async Task Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var query = _dbSet.AsNoTracking();
        var orderedQuery = GetDefaultOrder(query);

        return await orderedQuery.ToListAsync();
    }

    public virtual IQueryable<TEntity> GetDefaultOrder(IQueryable<TEntity> query)
    {
        return query;
    }

    public async Task<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        var query = _dbSet.AsNoTracking();

        return await query.FirstOrDefaultAsync(expression);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual Task Update(TEntity entity)
    {
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }

    public virtual async Task Save()
    {
        await _applicationDbContext.SaveChangesAsync();
    }
}
