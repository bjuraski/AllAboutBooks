using AllAboutBooksWebRazor_Temp.Data;
using AllAboutBooksWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AllAboutBooksWebRazor_Temp.Pages.Categories;

public class CreateModel : PageModel
{
    [BindProperty]
    public Category Category { get; set; } = new();

    private readonly ApplicationDbContext _applicationDbContext;

    public CreateModel(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(Category category)
    {
        await _applicationDbContext.Categories.AddAsync(category);
        await _applicationDbContext.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}
