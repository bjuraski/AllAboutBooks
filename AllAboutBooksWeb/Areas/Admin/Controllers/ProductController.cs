using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using AllAboutBooks.Models.ViewModels;
using AllAboutBooks.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = StaticDetails.RoleAdmin)]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        return View(products);
    }

    public async Task<IActionResult> Upsert(long? id)
    {
        var categorySelectList = await _unitOfWork.CategoryRepository.GetCategorySelectList();
        var productViewModel = new ProductViewModel
        {
            Product = new Product(),
            CategoryList = categorySelectList
        };

        if (id is null || id == 0)
        {
            return View(productViewModel);
        }

        productViewModel.Product = await _unitOfWork.ProductRepository.GetFirstOrDefaultByExpressionAsync(p => p.Id == id);

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
            await _unitOfWork.ProductRepository.InsertAsync(productViewModel.Product);
            await _unitOfWork.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index", "Product");
        }

        await _unitOfWork.ProductRepository.Update(productViewModel.Product);
        await _unitOfWork.Save();

        TempData["success"] = "Product updated successfully";

        return RedirectToAction("Index", "Product");
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        return Json(new { data = products });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long? id)
    {
        if (id is null || id == 0)
        {
            return Json(new { success = false, message = "Error while deleting. Product Id is not specified." });
        }

        var productToDelete = await _unitOfWork.ProductRepository.GetFirstOrDefaultByExpressionAsync(p => p.Id == id);

        if (productToDelete is null)
        {
            return Json(new { success = false, message = "Error while deleting. Product can't be found." });
        }

        var wwwRootPath = _webHostEnvironment.WebRootPath;
        var oldImagePath = Path.Combine(wwwRootPath, productToDelete.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }

        _unitOfWork.ProductRepository.Delete(productToDelete);
        await _unitOfWork.Save();

        return Json(new { success = true, message = "Product deleted successfully." });
    }

    #endregion
}
