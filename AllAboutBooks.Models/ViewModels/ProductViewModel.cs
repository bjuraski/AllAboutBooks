using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllAboutBooks.Models.ViewModels;

public class ProductViewModel
{
    public Product Product { get; set; }

    public IEnumerable<SelectListItem> CategoryList { get; set; }
}
