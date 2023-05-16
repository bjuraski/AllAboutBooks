using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllAboutBooksWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        return View(products);
    }

    public async Task<IActionResult> Details(long? id)
    {
        if (id is null || id == 0)
        {
            return NotFound(nameof(Product));
        }

        var product = await _unitOfWork.ProductRepository.GetFirstOrDefaultByExpressionAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound(nameof(Product));
        }

        var shoppingCart = new ShoppingCart
        {
            ProductId = id.Value,
            Product = product,
            Count = 1
        };

        return View(shoppingCart);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}