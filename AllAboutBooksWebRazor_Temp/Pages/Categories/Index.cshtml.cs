using AllAboutBooksWebRazor_Temp.Data;
using AllAboutBooksWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AllAboutBooksWebRazor_Temp.Pages.Categories;

public class IndexModel : PageModel
{
    public List<Category> CategoryList { get; set; } = new();

    private readonly ApplicationDbContext _applicationDbContext;

    public IndexModel(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task OnGet()
    {
        CategoryList = await _applicationDbContext
            .Categories
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
    }
}
