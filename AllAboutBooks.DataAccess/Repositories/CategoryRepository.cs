using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooks.DataAccess.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Save()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Update(Category category)
    {
        _applicationDbContext.Categories.Update(category);
    }

    public override IQueryable<Category> GetDefaultOrder(IQueryable<Category> query)
    {
        return base.GetDefaultOrder(query).OrderBy(c => c.DisplayOrder);
    }

    public void SetEntityStateAsDetached(Category category)
    {
        _applicationDbContext.Entry(category).State = EntityState.Detached;
    }
}
