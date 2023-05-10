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

    public void SetEntityStateAsDetached(Category category)
    {
        _applicationDbContext.Entry(category).State = EntityState.Detached;
    }

    public override IQueryable<Category> GetDefaultOrder(IQueryable<Category> query)
    {
        return base.GetDefaultOrder(query).OrderBy(c => c.DisplayOrder);
    }
}
