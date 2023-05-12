using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using AllAboutBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AllAboutBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IWebHostEnvironment webHostEnvironment)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAll();

        return View(products);
    }

    public async Task<IActionResult> Upsert(long? id)
    {
        var categories = await _categoryRepository.GetAll();
        var categorySelectList = categories.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        var productViewModel = new ProductViewModel
        {
            Product = new Product(),
            CategoryList = categorySelectList
        };

        if (id is null || id == 0)
        {
            return View(productViewModel);
        }

        productViewModel.Product = await _productRepository.GetByExpression(p => p.Id == id);

        return View(productViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(ProductViewModel productViewModel, IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            return View(productViewModel);
        }

        var wwwRootPath = _webHostEnvironment.WebRootPath;

        if (file != null)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var productPath = Path.Combine(wwwRootPath, @"images\product");

            if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
            {
                // Delete the old image
                var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            using var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create);

            file.CopyTo(fileStream);
            productViewModel.Product.ImageUrl = $@"\images\product\{fileName}";
        }

        if (productViewModel.Product.Id == 0)
        {
            await _productRepository.Add(productViewModel.Product);
            await _productRepository.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index", "Product");
        }

        await _productRepository.Update(productViewModel.Product);
        await _productRepository.Save();

        TempData["success"] = "Product updated successfully";

        return RedirectToAction("Index", "Product");
    }

    public async Task<IActionResult> Delete(long? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var product = await _productRepository.GetByExpression(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(long id)
    {
        var product = await _productRepository.GetByExpression(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        _productRepository.Remove(product);
        await _productRepository.Save();

        TempData["success"] = "Product deleted successfully";

        return RedirectToAction("Index", "Product");
    }
}
