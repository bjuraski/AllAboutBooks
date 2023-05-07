using AllAboutBooksWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooksWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
}
