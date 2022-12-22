using GroceryStore.Models.Home;
using Microsoft.AspNetCore.Mvc.Razor;

namespace GroceryStore.Models;

public abstract class ShoppingRazorPage<TModel> : RazorPage<TModel>
{
    public CartViewModel Cart => 
        ViewBag.CurrentCart as CartViewModel ?? 
        throw new InvalidOperationException("Shopping cart is missing");
}