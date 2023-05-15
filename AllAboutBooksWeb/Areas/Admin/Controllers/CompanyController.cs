using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using AllAboutBooks.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllAboutBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = StaticDetails.RoleAdmin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var companies = await _unitOfWork.CompanyRepository.GetAllAsync();

        return View(companies);
    }

    public async Task<IActionResult> Upsert(long? id)
    {
        if (!id.HasValue || id == 0)
        {
            var newCompany = new Company();

            return View(newCompany);
        }

        var company = await _unitOfWork.CompanyRepository.GetFirstOrDefaultByExpressionAsync(c => c.Id == id);

        return View(company);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Company company)
    {
        if (!ModelState.IsValid)
        {
            return View(company);
        }

        if (company.Id == 0)
        {
            await _unitOfWork.CompanyRepository.InsertAsync(company);
            await _unitOfWork.Save();

            TempData["success"] = "Company created successfully";

            return RedirectToAction("Index", "Company");
        }

        await _unitOfWork.CompanyRepository.Update(company);
        await _unitOfWork.Save();

        TempData["success"] = "Company updated successfully";

        return RedirectToAction("Index", "Company");
    }

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _unitOfWork.CompanyRepository.GetAllAsync();

        return Json(new { data = companies });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long? id)
    {
        if (!id.HasValue || id == 0)
        {
            return Json(new { success = false, message = "Error while deleting. Company Id is not specified." });
        }

        var companyToDelete = await _unitOfWork.CompanyRepository.GetFirstOrDefaultByExpressionAsync(c => c.Id == id);

        if (companyToDelete is null)
        {
            return Json(new { success = false, message = "Error while deleting. Company can't be found." });
        }

        _unitOfWork.CompanyRepository.Delete(companyToDelete);
        await _unitOfWork.Save();

        return Json(new { success = true, message = "Company deleted successfully" });
    }

    #endregion
}
