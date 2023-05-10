using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AllAboutBooks.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;
    internal DbSet<T> _dbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var query = _dbSet.AsNoTracking();
        var orderedQuery = GetDefaultOrder(query);

        return await orderedQuery.ToListAsync();
    }

    public virtual IQueryable<T> GetDefaultOrder(IQueryable<T> query)
    {
        return query;
    }

    public async Task<T?> GetByExpression(Expression<Func<T, bool>> expression)
    {
        var query = _dbSet.AsNoTracking();

        return await query.FirstOrDefaultAsync(expression);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
