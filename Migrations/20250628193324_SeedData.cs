using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BraveHeartBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Fire Safety Equipment" },
                    { 3, "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributes",
                columns: new[] { "Id", "DataType", "IsRequired", "Name", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, "string", true, "Color", 1 },
                    { 2, "string", true, "Storage", 1 },
                    { 3, "string", true, "Brand", 1 },
                    { 4, "string", true, "ExtinguisherClass", 2 },
                    { 5, "string", true, "Capacity", 2 },
                    { 6, "string", false, "Material", 2 },
                    { 7, "string", true, "Size", 3 },
                    { 8, "string", true, "Material", 3 },
                    { 9, "string", true, "Color", 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "ProductTypeId", "Stock" },
                values: new object[,]
                {
                    { 1, "https://example.com/iphone15.jpg", "iPhone 15", 999.00m, 1, 50 },
                    { 2, "https://example.com/galaxy-s24.jpg", "Samsung Galaxy S24", 899.00m, 1, 30 },
                    { 3, "https://example.com/macbook-pro.jpg", "MacBook Pro", 1999.00m, 1, 20 },
                    { 4, "https://example.com/abc-extinguisher.jpg", "ABC Fire Extinguisher", 89.99m, 2, 100 },
                    { 5, "https://example.com/co2-extinguisher.jpg", "CO2 Fire Extinguisher", 149.99m, 2, 75 },
                    { 6, "https://example.com/fire-blanket.jpg", "Fire Blanket", 29.99m, 2, 200 },
                    { 7, "https://example.com/firefighter-jacket.jpg", "Firefighter Jacket", 299.99m, 3, 25 },
                    { 8, "https://example.com/safety-helmet.jpg", "Safety Helmet", 89.99m, 3, 60 }
                });

            migrationBuilder.InsertData(
                table: "ProductAttributeValues",
                columns: new[] { "Id", "ProductAttributeId", "ProductId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "Black" },
                    { 2, 1, 1, "White" },
                    { 3, 2, 1, "128GB" },
                    { 4, 2, 1, "256GB" },
                    { 5, 3, 1, "Apple" },
                    { 6, 1, 2, "Black" },
                    { 7, 1, 2, "Blue" },
                    { 8, 2, 2, "128GB" },
                    { 9, 2, 2, "512GB" },
                    { 10, 3, 2, "Samsung" },
                    { 11, 1, 3, "Space Gray" },
                    { 12, 1, 3, "Silver" },
                    { 13, 2, 3, "512GB" },
                    { 14, 2, 3, "1TB" },
                    { 15, 3, 3, "Apple" },
                    { 16, 4, 4, "ABC" },
                    { 17, 5, 4, "5 lbs" },
                    { 18, 5, 4, "10 lbs" },
                    { 19, 6, 4, "Steel" },
                    { 20, 4, 5, "CO2" },
                    { 21, 5, 5, "10 lbs" },
                    { 22, 6, 5, "Aluminum" },
                    { 23, 4, 6, "N/A" },
                    { 24, 5, 6, "4ft x 6ft" },
                    { 25, 6, 6, "Fiberglass" },
                    { 26, 7, 7, "Large" },
                    { 27, 7, 7, "XL" },
                    { 28, 8, 7, "Nomex" },
                    { 29, 9, 7, "Yellow" },
                    { 30, 9, 7, "Orange" },
                    { 31, 7, 8, "Medium" },
                    { 32, 7, 8, "Large" },
                    { 33, 8, 8, "ABS Plastic" },
                    { 34, 9, 8, "White" },
                    { 35, 9, 8, "Yellow" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ProductAttributeValues",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductAttributes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
