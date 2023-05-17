using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

    public async Task<IActionResult> Details(long? productId)
    {
        if (!productId.HasValue || productId == 0)
        {
            return NotFound(nameof(Product));
        }

        var product = await _unitOfWork.ProductRepository.GetFirstOrDefaultByExpressionAsync(p => p.Id == productId, false);

        if (product is null)
        {
            return NotFound(nameof(Product));
        }

        var claimsIdentity = (ClaimsIdentity)User.Identity;

        if (claimsIdentity.IsAuthenticated)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cartFromDb = await _unitOfWork.ShoppingCartRepository
                .GetFirstOrDefaultByExpressionAsync(sc => sc.ApplicationUserId == userId && sc.ProductId == productId, false);

            if (cartFromDb is null)
            {
                var shoppingCart = new ShoppingCart
                {
                    ProductId = productId.Value,
                    Product = product,
                    Count = 1
                };

                return View(shoppingCart);
            }

            return View(cartFromDb);
        }

        var notAutheticatedshoppingCart = new ShoppingCart
        {
            ProductId = productId.Value,
            Product = product,
            Count = 1
        };

        return View(notAutheticatedshoppingCart);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        shoppingCart.ApplicationUserId = userId;

        var cartFromDb = await _unitOfWork.ShoppingCartRepository
            .GetFirstOrDefaultByExpressionAsync(sc => sc.ApplicationUserId == userId && sc.ProductId == shoppingCart.ProductId, false);

        if (cartFromDb is not null)
        {
            cartFromDb.Count = shoppingCart.Count;

            await _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
            await _unitOfWork.Save();
        }
        else
        {
            await _unitOfWork.ShoppingCartRepository.InsertAsync(shoppingCart);
            await _unitOfWork.Save();
        }

        TempData["success"] = "Shopping cart updated successfully";

        return RedirectToAction(nameof(Index));
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