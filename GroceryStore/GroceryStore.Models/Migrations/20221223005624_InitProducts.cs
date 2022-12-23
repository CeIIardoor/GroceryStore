using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryStore.Models.Migrations
{
    public partial class InitProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Sort = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ExternalPaymentId = table.Column<string>(type: "TEXT", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingStreet = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingCity = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingStateOrProvince = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingPostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingCountry = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    Description = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    DiscountPrice = table.Column<double>(type: "REAL", nullable: true),
                    CurrentInventory = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: false),
                    ProductOptionName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AdditionalCost = table.Column<double>(type: "REAL", nullable: false),
                    CurrentInventory = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOption_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProductOptionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_ProductOption_ProductOptionId",
                        column: x => x.ProductOptionId,
                        principalTable: "ProductOption",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCartItem_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[] { 1, "Other", 5 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[] { 2, "Fruits", 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[] { 3, "Vegetables", 2 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[] { 4, "Drinks", 3 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Sort" },
                values: new object[] { 5, "Meat", 4 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 1, 100, "The Pringles potato chip is a snack food that was invented in 1967. The chip is made of potato flakes, vegetable oil, salt, and flavorings.", null, "Pringles.jpg", "Procter & Gamble", "Pringles", 10.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 2, null, "This slice is from is a large, round, juicy melon with a light green rind and sweet, red flesh. It is a variety of the species Citrullus lanatus.", null, "WaterMelon.jpg", "South Africa", "WaterMelon Slice", 15.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 3, 100, "The avocado is a tree native to Mexico and Central America. It is classified as a member of the flowering plant family Lauraceae.", null, "Avocado.jpg", "Mexico", "Avocado", 10.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 4, 100, "Beef stock is a stock made from beef bones, meat, and vegetables. It is used as a base for soups, sauces, and stews.", null, "BeefStock.jpg", "Unilever", "Beef Stock", 30.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 5, 100, "Ananas comosus, commonly known as pineapple or ananas, is a tropical plant with an edible multiple fruit consisting of coalesced berries.", null, "Ananas.jpg", "Brazil", "PineApple", 15.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 6, 100, "Valencia Abtal is a very popular drink among moroccan students. Due to its low price, and serving as a good dekka for mid class breaks.", null, "Abtal.jpg", "Valencia", "Abtal", 2.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 7, null, "Coca-Cola is a carbonated soft drink manufactured by The Coca-Cola Company. Originally intended as a patent medicine.", 4.0, "CocaCola.jpg", "Coca-Cola Company", "Coca Cola", 5.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 8, 100, "Asta Coffee is a brand of coffee produced by the Asta Coffee Company, a subsidiary of the Nestlé Group.", 19.0, "Asta.jpg", "Nestlé", "Asta Coffee", 25.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CurrentInventory", "Description", "DiscountPrice", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 9, 100, "Lettuce is most often used for salads, although it is also seen in other kinds of food, such as soups, sandwiches, and wraps.", null, "Lettuce.jpg", "Morocco", "Lettuce", 7.0 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 1, 8 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 2, 6 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 2, 9 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 4, 7 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 4, 8 });

            migrationBuilder.InsertData(
                table: "CategoryProduct",
                columns: new[] { "CategoriesId", "ProductsId" },
                values: new object[] { 5, 4 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 3, 5.0, 60, "Medium", 1 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 4, 10.0, 20, "Large", 1 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 5, 20.0, 20, "Family Size", 1 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 6, 0.0, 20, "Small Can (33cl)", 7 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 7, 2.0, 20, "Medium Bottle (50cl)", 7 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 8, 4.0, 20, "Large Bottle (1L)", 7 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 9, 6.0, 20, "X-Large Bottle (1.5L)", 7 });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "Id", "AdditionalCost", "CurrentInventory", "Name", "ProductId" },
                values: new object[] { 10, 8.0, 20, "Family Size Bottle (2L)", 7 });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOption_ProductId",
                table: "ProductOption",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ProductId",
                table: "ShoppingCartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ProductOptionId",
                table: "ShoppingCartItem",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItem_ShoppingCartId",
                table: "ShoppingCartItem",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ShoppingCartItem");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductOption");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
