using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public override IQueryable<ShoppingCart> ConfigureIncludes(IQueryable<ShoppingCart> query)
    {
        return base.ConfigureIncludes(query).Include(sc => sc.Product).ThenInclude(p => p.Category);
    }
}
