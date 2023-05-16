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

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbSet;

        query = ConfigureIncludes(query);
        query = GetDefaultOrder(query);

        return await query.ToListAsync();
    }

    public virtual IQueryable<TEntity> ConfigureIncludes(IQueryable<TEntity> query)
    {
        return query;
    }

    public virtual IQueryable<TEntity> GetDefaultOrder(IQueryable<TEntity> query)
    {
        return query;
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<TEntity> GetFirstOrDefaultByExpressionAsync(Expression<Func<TEntity, bool>> expression, bool shouldBeTracked = true)
    {
        IQueryable<TEntity> query = _dbSet;

        if (!shouldBeTracked)
        {
            query = query.AsNoTracking();
        }

        query = ConfigureIncludes(query);

        return await query.FirstOrDefaultAsync(expression);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual Task Update(TEntity entity)
    {
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }
}
