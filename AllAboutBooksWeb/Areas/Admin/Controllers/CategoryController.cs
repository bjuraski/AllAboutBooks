using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _unitOfWork.CategoryRepository.GetAll();

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
            ModelState.AddModelError(nameof(category.Name), "The Display Order cannot exactly match the Name");
        }

        if (ModelState.IsValid)
        {
            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Save();

            TempData["success"] = "Category created successfully";

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

        var category = await _unitOfWork.CategoryRepository.GetByExpression(c => c.Id == id);

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
            ModelState.AddModelError(nameof(category.Name), "The Display Order cannot exactly match the Name");
        }

        var categoryWithSameDisplayOrder = await _unitOfWork.CategoryRepository
            .GetByExpression(c => c.DisplayOrder == category.DisplayOrder && c.Id != category.Id);


        if (categoryWithSameDisplayOrder is not null)
        {
            ModelState.AddModelError(nameof(category.DisplayOrder), "The Display Order value already exists");
        }

        if (ModelState.IsValid)
        {
            var categoryInDb = await _unitOfWork.CategoryRepository.GetByExpression(c => c.Id == category.Id);

            if (categoryInDb is not null)
            {
                _unitOfWork.CategoryRepository.SetEntityStateAsDetached(categoryInDb);
            }

            await _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Save();

            TempData["success"] = "Category updated successfully";

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

        var category = await _unitOfWork.CategoryRepository.GetByExpression(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePOST(long id)
    {
        var category = await _unitOfWork.CategoryRepository.GetByExpression(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        _unitOfWork.CategoryRepository.Remove(category);
        await _unitOfWork.Save();

        TempData["success"] = "Category deleted successfully";

        return RedirectToAction("Index", "Category");
    }
}
