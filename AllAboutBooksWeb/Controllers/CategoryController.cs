using AllAboutBooksWeb.Data;
using AllAboutBooksWeb.Models;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await _applicationDbContext.AddAsync(category);
        await _applicationDbContext.SaveChangesAsync();

        return RedirectToAction("Index", "Category");
    }
}
