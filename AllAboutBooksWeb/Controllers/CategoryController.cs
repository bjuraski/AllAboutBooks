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
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError(nameof(Category.Name), "The Display Order cannot exactly match the Name");
        }

        if (ModelState.IsValid)
        {
            await _applicationDbContext.Categories.AddAsync(category);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }

        return View(category);
    }

    public async Task<IActionResult> Edit(long? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var category = await _applicationDbContext
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError(nameof(Category.Name), "The Display Order cannot exactly match the Name");
        }

        var categoryWithSameDisplayOrder = await _applicationDbContext
            .Categories
            .FirstOrDefaultAsync(c => c.DisplayOrder == category.DisplayOrder && c.Id != category.Id);

        if (categoryWithSameDisplayOrder is not null)
        {
            ModelState.AddModelError(nameof(Category.DisplayOrder), "The Display Order value already exists");
        }

        if (ModelState.IsValid)
        {
            var categoryInDb = await _applicationDbContext
                .Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (categoryInDb is not null)
            {
                _applicationDbContext.Entry(categoryInDb).State = EntityState.Detached;
            }

            _applicationDbContext.Categories.Update(category);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }

        return View(category);
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var category = await _applicationDbContext
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(long? id)
    {
        var category = await _applicationDbContext
            .Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        _applicationDbContext.Categories.Remove(category);
        await _applicationDbContext.SaveChangesAsync();

        return RedirectToAction("Index", "Category");
    }
}
