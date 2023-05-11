using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.Models;

namespace AllAboutBooks.DataAccess.Repositories.Interfaces;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
