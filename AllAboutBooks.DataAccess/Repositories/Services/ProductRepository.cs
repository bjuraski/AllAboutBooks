using AllAboutBooks.DataAccess.Data;
using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooks.DataAccess.Repositories.Services;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public override IQueryable<Product> ConfigureIncludes(IQueryable<Product> query)
    {
        return base.ConfigureIncludes(query).Include(p => p.Category);
    }

    public override async Task Update(Product entity)
    {
        var productFromDb = await _applicationDbContext
            .Products
            .FirstOrDefaultAsync(p => p.Id == entity.Id);

        if (productFromDb != null)
        {
            productFromDb.Title = entity.Title;
            productFromDb.Author = entity.Author;
            productFromDb.Description = entity.Description;
            productFromDb.InternationalStandardBookNumber = entity.InternationalStandardBookNumber;
            productFromDb.ListPrice = entity.ListPrice;
            productFromDb.Price = entity.Price;
            productFromDb.Price50 = entity.Price50;
            productFromDb.Price100 = entity.Price100;
            productFromDb.CategoryId = entity.CategoryId;
            productFromDb.ImageUrl = entity.ImageUrl;
        }
    }
}
