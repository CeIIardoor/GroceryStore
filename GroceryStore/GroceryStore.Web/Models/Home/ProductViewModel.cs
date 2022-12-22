using System.Linq;
using Microsoft.AspNetCore.Html;

namespace GroceryStore.Models.Home;

public class ProductViewModel
{
    public Product Info { get; set; } = null!;
    
    public bool IsOnSale => Info.DiscountPrice.HasValue;
    public bool HasOptions => Info.Options.Any();
    public bool ShouldRenderCartButton { get; set; }
    public bool Swap { get; set; }

    public HtmlString PriceDisplay
    {
        get
        {
            if (HasOptions)
            {
                var price = IsOnSale ? Info.DiscountPrice : Info.Price;
                var prices = Info.Options.Select(p => price + p.AdditionalCost).ToList();
                var min = prices.Min();
                var max = prices.Max();

                return new HtmlString($"{min:###.00} MAD - {max:###.00} MAD");
            }

            return IsOnSale
                ? new HtmlString($"<span class=\"text-muted text-decoration-line-through\">{Info.Price:###.00} MAD</span> {Info.DiscountPrice:###.00} MAD")
                : new HtmlString($"{Info.Price:###.00} MAD");
        }
    }
}