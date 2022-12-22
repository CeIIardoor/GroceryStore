using System.Linq;
using GroceryStore.Models.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Models;

public class  StoreDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlite("Data Source=GroceryStore/GroceryStore.Web/GroceryStore.Products.db")
            .LogTo(Console.WriteLine);
    //Data Source=GroceryStore.Products.db for windows
    public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // case insentive
        modelBuilder.Entity<Product>().Property(c => c.Name).UseCollation("NOCASE");
        modelBuilder.Entity<Product>().Property(c => c.Description).UseCollation("NOCASE");
        
        modelBuilder.Entity<Category>()
            .HasData(
                new Category {Id = 1, Name = "Other", Sort = 5},
                new Category {Id = 2, Name = "Fruits", Sort = 1},
                new Category {Id = 3, Name = "Vegetables", Sort = 2 },
                new Category {Id = 4, Name = "Drinks", Sort = 3 },
                new Category {Id = 5, Name = "Meat", Sort = 4 }
            );
        
        modelBuilder.Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Pringles",
                    Description =
                        "The Pringles potato chip is a snack food that was invented in 1967. " + 
                        "The chip is made of potato flakes, vegetable oil, salt, and flavorings.",
                    Manufacturer = "Procter & Gamble", 
                    Price = 10, 
                    CurrentInventory = 100,
                    ImageUrl = "Pringles.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "WaterMelon Slice",
                    Description =
                        "This slice is from is a large, round, juicy melon with a light green rind and sweet, red flesh. " +
                        "It is a variety of the species Citrullus lanatus.",
                    Manufacturer = "South Africa",
                    ImageUrl = "WaterMelon.jpg",
                    Price = 15
                },
                new Product
                {
                    Id = 3,
                    Name = "Avocado",
                    Description = "The avocado is a tree native to Mexico and Central America. " +
                                  "It is classified as a member of the flowering plant family Lauraceae.",
                    Manufacturer = "Mexico",
                    Price = 10,
                    CurrentInventory = 100,
                    ImageUrl = "Avocado.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Beef Stock",
                    Description =
                        "Beef stock is a stock made from beef bones, meat, and vegetables. " 
                        + "It is used as a base for soups, sauces, and stews.",
                    Manufacturer = "Unilever",
                    Price = 30,
                    CurrentInventory = 100,
                    ImageUrl = "BeefStock.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "PineApple",
                    Description =
                        "Ananas comosus, commonly known as pineapple or ananas," +
                        " is a tropical plant with an edible multiple fruit consisting of coalesced berries.",
                    ImageUrl = "Ananas.jpg",
                    Manufacturer = "Brazil",
                    Price = 15,
                    CurrentInventory = 100
                },
                new Product
                {
                    Id = 6,
                    Name = "Abtal",
                    Description =
                        "Valencia Abtal is a very popular drink among moroccan students. " +
                        "Due to its low price, and serving as a good dekka for mid class breaks.",
                    Manufacturer = "Valencia", Price = 2, CurrentInventory = 100,
                    ImageUrl = "Abtal.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "Coca Cola",
                    Description = "Coca-Cola is a carbonated soft drink manufactured by The Coca-Cola Company. " + 
                                  "Originally intended as a patent medicine.",
                                  
                    Manufacturer = "Coca-Cola Company", 
                    Price = 5,
                    DiscountPrice = 4,
                    ImageUrl = "CocaCola.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "Asta Coffee",
                    Description = "Asta Coffee is a brand of coffee produced by the Asta Coffee Company," +
                                  " a subsidiary of the Nestlé Group.",
                    Manufacturer = "Nestlé", Price = 25, DiscountPrice = 19, CurrentInventory = 100,
                    ImageUrl = "Asta.jpg"
                },
                new Product
                {
                    Id = 9,
                    Name = "Lettuce",
                    Description =
                        "Lettuce is most often used for salads, although it is also seen in other kinds of food," +
                        " such as soups, sandwiches, and wraps.",
                    Manufacturer = "Morocco", Price = 7, CurrentInventory = 100,
                    ImageUrl = "Lettuce.jpg"
                }
            );

        modelBuilder.Entity("CategoryProduct")
            .HasData(
                new {ProductsId = 1, CategoriesId = 1},
                new {ProductsId = 2, CategoriesId = 2},
                new {ProductsId = 3, CategoriesId = 2},
                new {ProductsId = 4, CategoriesId = 1},
                new {ProductsId = 4, CategoriesId = 5},
                new {ProductsId = 5, CategoriesId = 1},
                new {ProductsId = 6, CategoriesId = 1},
                new {ProductsId = 6, CategoriesId = 2},
                new {ProductsId = 7, CategoriesId = 4},
                new {ProductsId = 8, CategoriesId = 1},
                new {ProductsId = 8, CategoriesId = 4},
                new {ProductsId = 9, CategoriesId = 2}
            );

        modelBuilder
            .Entity<ProductOption>()
            .HasData(
                // Pringles options
                new() {Id = 3, Name = "Medium", CurrentInventory = 60, AdditionalCost = 5, ProductId = 1},
                new() {Id = 4, Name = "Large", CurrentInventory = 20, AdditionalCost = 10, ProductId = 1},
                new() {Id = 5, Name = "Family Size", CurrentInventory = 20, AdditionalCost = 20, ProductId = 1},
                // Coca Cola options
                new() {Id = 6, Name = "Small Can (33cl)", CurrentInventory = 20, ProductId = 7},
                new() {Id = 7, Name = "Medium Bottle (50cl)", CurrentInventory = 20,AdditionalCost = 2, ProductId = 7},
                new() {Id = 8, Name = "Large Bottle (1L)", CurrentInventory = 20, AdditionalCost = 4, ProductId = 7},
                new() {Id = 9, Name = "X-Large Bottle (1.5L)", CurrentInventory = 20, AdditionalCost = 6, ProductId = 7},
                new() {Id = 10, Name = "Family Size Bottle (2L)", CurrentInventory = 20, AdditionalCost = 8, ProductId = 7}
            );

        modelBuilder
            .Entity<ShoppingCart>()
            .Navigation(x => x.Items).AutoInclude();

        modelBuilder
            .Entity<ShoppingCartItem>()
            .Navigation(x => x.Product).AutoInclude();

        modelBuilder.Entity<Product>()
            .Navigation(x => x.Options).AutoInclude();
    }

    public async Task<(Product? product, ProductOption? option)> 
        UpdateShoppingCart(int productId, int? productOptionId, int shoppingCartId, int quantity)
    {
        var product = await Products
            .Include(p => p.Options)
            .Include(p => p.Categories)
            .FirstOrDefaultAsync(p => p.Id == productId);

        var option = productOptionId.HasValue
            ? product?.Options.FirstOrDefault(p => p.Id == productOptionId)
            : null;

        var cart = await ShoppingCarts.FindAsync(shoppingCartId);

        if (cart is null || product is null) 
            return (product, option);
        
        var item = cart.Items
            .Where(i => i.ProductId == product.Id)
            .If(option != null, q => q.Where(p => p.ProductOptionId == option?.Id))
            .FirstOrDefault();

        if (item == null)
        {
            item = new ShoppingCartItem
            {
                Product = product,
                Option = option
            };
            cart.Items.Add(item);
        }

        item.Quantity = quantity;

        if (quantity == 0)
        {
            cart.Items.Remove(item);
        }

        await SaveChangesAsync();

        return (product, option);
    }
}