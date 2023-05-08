using AllAboutBooksWebRazor_Temp.Data;
using AllAboutBooksWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooksWebRazor_Temp.Pages.Categories;

public class EditModel : PageModel
{
    [BindProperty]
    public Category Category { get; set; } = new();

    private readonly ApplicationDbContext _applicationDbContext;

    public EditModel(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IActionResult> OnGet(long? id)
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

        Category = category;

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            _applicationDbContext.Categories.Update(Category);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        return Page();
    }
}
