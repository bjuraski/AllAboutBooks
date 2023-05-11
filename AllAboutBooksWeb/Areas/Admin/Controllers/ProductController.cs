using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAll();

        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productRepository.Add(product);
            await _productRepository.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index", "Product");
        }

        return View(product);
    }

    public async Task<IActionResult> Edit(long? id)
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

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        var productWithSameTitle = await _productRepository
            .GetByExpression(p => p.Title == product.Title && p.Id != product.Id);

        if (productWithSameTitle is not null)
        {
            ModelState.AddModelError(nameof(product.Title), "The product with the same title already exists");
        }

        if (ModelState.IsValid)
        {
            _productRepository.Update(product);
            await _productRepository.Save();

            TempData["success"] = "Product updated successfully";

            return RedirectToAction("Index", "Product");
        }

        return View(product);
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
