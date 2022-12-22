using GroceryStore.Models;
using GroceryStore.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Controllers;

[Route("[controller]")]
public class ProductsController : Controller
{
    private readonly StoreDbContext _db;

    public ProductsController(StoreDbContext db)
    {
        this._db = db;
    }
    
    [HttpGet, Route("{id}")]
    public async Task<IActionResult> Show(int id, int? productOptionId, bool? showModal)
    {
        var product = await _db.Products
            .Include(p => p.Options)
            .Include(p => p.Categories)
            .Where(p => p.Id == id)
            .Select(p => new ProductWithOptionsViewModel { Info = p, ProductOptionId = productOptionId })
            .FirstOrDefaultAsync();
        
        if (product == null)
            return NotFound();

        // set a default selected option if one isn't provided
        product.ProductOptionId ??= product.Info.Options.Select(o => o.Id).FirstOrDefault();
        product.InstantlyShowModal = showModal.HasValue && showModal.Value;
        product.Swap = true;
        
        return PartialView("_ProductOptions", product);
    }
}