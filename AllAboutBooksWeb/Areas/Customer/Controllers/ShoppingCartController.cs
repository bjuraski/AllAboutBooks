using AllAboutBooks.DataAccess.Repositories.Interfaces;
using AllAboutBooks.Models;
using AllAboutBooks.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllAboutBooksWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class ShoppingCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        var userShoppingCarts = await _unitOfWork.ShoppingCartRepository.GetAllByExpressionAsync(sc => sc.ApplicationUserId == userId, false);
        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCartList = userShoppingCarts
        };

        foreach (var shoppingCart in shoppingCartViewModel.ShoppingCartList)
        {
            shoppingCart.Price = GetPriceBasedOnQuantity(shoppingCart);
            shoppingCartViewModel.OrderTotal += (shoppingCart.Price * shoppingCart.Count);
        }

        return View(shoppingCartViewModel);
    }

    public async Task<IActionResult> Increment(long cartId)
    {
        var cartFromDb = await _unitOfWork.ShoppingCartRepository.GetFirstOrDefaultByExpressionAsync(sc => sc.Id == cartId);

        if (cartFromDb is null)
        {
            return RedirectToAction(nameof(Index));
        }

        cartFromDb.Count += 1;

        await _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
        await _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Decrement(long cartId)
    {
        var cartFromDb = await _unitOfWork.ShoppingCartRepository.GetFirstOrDefaultByExpressionAsync(sc => sc.Id == cartId);

        if (cartFromDb is null)
        {
            return RedirectToAction(nameof(Index));
        }

        if (cartFromDb.Count <= 1)
        {
            _unitOfWork.ShoppingCartRepository.Delete(cartFromDb);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        cartFromDb.Count -= 1;

        await _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
        await _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Remove(long cartId)
    {
        var cartFromDb = await _unitOfWork.ShoppingCartRepository.GetFirstOrDefaultByExpressionAsync(sc => sc.Id == cartId);

        if (cartFromDb is null)
        {
            return RedirectToAction(nameof(Index));
        }

        _unitOfWork.ShoppingCartRepository.Delete(cartFromDb);
        await _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    private static double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
    {
        var product = shoppingCart.Product;
        var quantity = shoppingCart.Count;

        return quantity <= 50
            ? product.Price
            : quantity <= 100
                ? product.Price50
                : product.Price100;
    }
}
