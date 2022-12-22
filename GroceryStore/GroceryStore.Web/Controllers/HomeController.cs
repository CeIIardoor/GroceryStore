using System.Diagnostics;
using Htmx;
using GroceryStore.Models;
using GroceryStore.Models.Extensions;
using GroceryStore.Models.Home;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StoreDbContext _db;

    public HomeController(ILogger<HomeController> logger, StoreDbContext db)
    {
        this._logger = logger;
        this._db = db;
    }

    [HttpGet, Route("")]
    public async Task<IActionResult> Index(string? category, string? query)
    {
        Category? categoryRecord = null;
        var hasCategory = false;
        if (!string.IsNullOrWhiteSpace(category)) 
        {
            categoryRecord = await _db.Categories.FirstOrDefaultAsync(c => c.Name == category);
            hasCategory = categoryRecord != null;
        }

        var hasQuery = !string.IsNullOrWhiteSpace(query);

        var results =
            await _db.Products
                .Include(x => x.Options)
                .If(hasCategory, q => q.Where(p => p.Categories.Contains(categoryRecord!)))
                .If(hasQuery, q => q.Where(p => EF.Functions.Like(p.Name, $"%{query}%") ))
                .Select(p => new ProductViewModel { Info = p })
                .ToListAsync();

        var model = new IndexViewModel(results, categoryRecord, query);
        
        Response.Htmx(h => {
            // we want to push the current url 
            // into the history
            h.PushUrl(Request.GetEncodedUrl());
        });

        return Request.IsHtmx()
            ? PartialView("_Products", model)
            : View(model);
    }

    [HttpGet, Route("privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet, Route("error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}

internal class StoreUser
{
}