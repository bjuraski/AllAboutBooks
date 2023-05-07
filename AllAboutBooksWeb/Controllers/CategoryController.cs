using AllAboutBooksWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooksWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CategoryController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _applicationDbContext
            .Categories
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();

        return View(categories);
    }
}
