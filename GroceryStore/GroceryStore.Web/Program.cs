using GroceryStore.Models;
using GroceryStore.Models.Cart;
using GroceryStore.Web.Areas.Identity.Data.GroceryStore;
using Microsoft.EntityFrameworkCore;
using GroceryStore.Web.Data.GroceryStore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AuthDbContext>(
    options => options.UseSqlite("Data Source=GroceryStore/GroceryStore.Web/GroceryStore.Users.db")
    );
builder.Services.AddDefaultIdentity<User>(
        options => options.SignIn.RequireConfirmedAccount = false
        ).AddEntityFrameworkStores<AuthDbContext>();
// Add services to the container.
builder.Services
    .AddControllersWithViews(
        options => {
        // will get the cart once we've executed an action
        options.Filters.Add<ShoppingCartViewModelFilter>();
    }).AddRazorRuntimeCompilation();

builder.Services.AddDbContext<StoreDbContext>();
builder.Services.AddShoppingCart();

var app = builder.Build();

// migrate to latest database version

 using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseShoppingCart();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();