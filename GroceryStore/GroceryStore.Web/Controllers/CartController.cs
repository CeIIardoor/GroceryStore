using Htmx;
using GroceryStore.Models;
using GroceryStore.Models.Cart;
using GroceryStore.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private readonly StoreDbContext _db;
    private readonly CurrentShoppingCart _currentShoppingCart;

    public CartController(StoreDbContext db, CurrentShoppingCart currentShoppingCart)
    {
        this._db = db;
        this._currentShoppingCart = currentShoppingCart;
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Add([FromForm] UpdateCartRequest input)
    {
        if (input.Remove)
        {
            input.Quantity = 0;
        }

        var (product, option) =
            await _db.UpdateShoppingCart(
                input.ProductId,
                input.ProductOptionId,
                _currentShoppingCart.Id,
                input.Quantity
            );

        if (option is not null)
        {
            return PartialView("_ProductOptions", new ProductWithOptionsViewModel
            {
                Info = product!,
                ProductOptionId = option.Id,
                ShouldRenderCartButton = true,
                InstantlyShowModal = true,
                Swap = true
            });
        }

        // added via card or view options
        return PartialView("_Product", new ProductViewModel
        {
            Info = product!,
            ShouldRenderCartButton = true,
            Swap = true
        });
    }

    [HttpPut, Route("")]
    public async Task<IActionResult> Update([FromForm] UpdateCartRequest input)
    {
        if (input.Remove)
        {
            input.Quantity = 0;
        }

        await _db.UpdateShoppingCart(
            input.ProductId,
            input.ProductOptionId,
            _currentShoppingCart.Id,
            input.Quantity
        );

        // We should update the UI
        var product = await _db.Products.FindAsync(input.ProductId);
        var model = new ProductViewModel
        {
            Info = product!,
            ShouldRenderCartButton = true
        };

        return PartialView("_CartItems", model);
    }

    [HttpGet, Route("")]
    public IActionResult Show()
    {
        return PartialView("_CartItems");
    }

    [HttpDelete, Route("")]
    public async Task<IActionResult> Delete()
    {
        var cart = await _db.ShoppingCarts.FindAsync(_currentShoppingCart.Id);
        cart?.Items.Clear();
        await _db.SaveChangesAsync();
        
        // force page to do a complete refresh
        Response.Htmx(htmx => {
            htmx.Refresh();
        });

        return PartialView("_CartItems");
    }
}