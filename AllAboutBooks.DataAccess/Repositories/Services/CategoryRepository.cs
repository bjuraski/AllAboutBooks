using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooks.DataAccess.Repositories.Services;

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
        => base.GetDefaultOrder(query).OrderBy(c => c.DisplayOrder);

    public async Task<IEnumerable<SelectListItem>> GetCategorySelectList()
        => await _applicationDbContext
            .Categories
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .ToListAsync();
}
