using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
