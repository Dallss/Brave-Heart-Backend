using Microsoft.EntityFrameworkCore;
using BraveHeartBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BraveHeartBackend.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Register your models here
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CheckoutDetails> CheckoutDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed ProductTypes
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Electronics" },
                new ProductType { Id = 2, Name = "Fire Safety Equipment" },
                new ProductType { Id = 3, Name = "Clothing" }
            );

            // Seed ProductAttributes
            modelBuilder.Entity<ProductAttribute>().HasData(
                // Electronics attributes
                new ProductAttribute { Id = 1, Name = "Color", DataType = "string", IsRequired = true, ProductTypeId = 1 },
                new ProductAttribute { Id = 2, Name = "Storage", DataType = "string", IsRequired = true, ProductTypeId = 1 },
                new ProductAttribute { Id = 3, Name = "Brand", DataType = "string", IsRequired = true, ProductTypeId = 1 },
                
                // Fire Safety attributes
                new ProductAttribute { Id = 4, Name = "ExtinguisherClass", DataType = "string", IsRequired = true, ProductTypeId = 2 },
                new ProductAttribute { Id = 5, Name = "Capacity", DataType = "string", IsRequired = true, ProductTypeId = 2 },
                new ProductAttribute { Id = 6, Name = "Material", DataType = "string", IsRequired = false, ProductTypeId = 2 },
                
                // Clothing attributes
                new ProductAttribute { Id = 7, Name = "Size", DataType = "string", IsRequired = true, ProductTypeId = 3 },
                new ProductAttribute { Id = 8, Name = "Material", DataType = "string", IsRequired = true, ProductTypeId = 3 },
                new ProductAttribute { Id = 9, Name = "Color", DataType = "string", IsRequired = true, ProductTypeId = 3 }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Electronics products
                new Product { Id = 1, Name = "iPhone 15", Price = 999.00m, Stock = 50, ProductTypeId = 1, ImageUrl = "https://example.com/iphone15.jpg" },
                new Product { Id = 2, Name = "Samsung Galaxy S24", Price = 899.00m, Stock = 30, ProductTypeId = 1, ImageUrl = "https://example.com/galaxy-s24.jpg" },
                new Product { Id = 3, Name = "MacBook Pro", Price = 1999.00m, Stock = 20, ProductTypeId = 1, ImageUrl = "https://example.com/macbook-pro.jpg" },
                
                // Fire Safety products
                new Product { Id = 4, Name = "ABC Fire Extinguisher", Price = 89.99m, Stock = 100, ProductTypeId = 2, ImageUrl = "https://example.com/abc-extinguisher.jpg" },
                new Product { Id = 5, Name = "CO2 Fire Extinguisher", Price = 149.99m, Stock = 75, ProductTypeId = 2, ImageUrl = "https://example.com/co2-extinguisher.jpg" },
                new Product { Id = 6, Name = "Fire Blanket", Price = 29.99m, Stock = 200, ProductTypeId = 2, ImageUrl = "https://example.com/fire-blanket.jpg" },
                
                // Clothing products
                new Product { Id = 7, Name = "Firefighter Jacket", Price = 299.99m, Stock = 25, ProductTypeId = 3, ImageUrl = "https://example.com/firefighter-jacket.jpg" },
                new Product { Id = 8, Name = "Safety Helmet", Price = 89.99m, Stock = 60, ProductTypeId = 3, ImageUrl = "https://example.com/safety-helmet.jpg" }
            );

            // Seed ProductAttributeValues
            modelBuilder.Entity<ProductAttributeValue>().HasData(
                // iPhone 15 attribute values
                new ProductAttributeValue { Id = 1, Value = "Black", ProductId = 1, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 2, Value = "White", ProductId = 1, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 3, Value = "128GB", ProductId = 1, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 4, Value = "256GB", ProductId = 1, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 5, Value = "Apple", ProductId = 1, ProductAttributeId = 3 },
                
                // Samsung Galaxy S24 attribute values
                new ProductAttributeValue { Id = 6, Value = "Black", ProductId = 2, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 7, Value = "Blue", ProductId = 2, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 8, Value = "128GB", ProductId = 2, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 9, Value = "512GB", ProductId = 2, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 10, Value = "Samsung", ProductId = 2, ProductAttributeId = 3 },
                
                // MacBook Pro attribute values
                new ProductAttributeValue { Id = 11, Value = "Space Gray", ProductId = 3, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 12, Value = "Silver", ProductId = 3, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 13, Value = "512GB", ProductId = 3, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 14, Value = "1TB", ProductId = 3, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 15, Value = "Apple", ProductId = 3, ProductAttributeId = 3 },
                
                // ABC Fire Extinguisher attribute values
                new ProductAttributeValue { Id = 16, Value = "ABC", ProductId = 4, ProductAttributeId = 4 },
                new ProductAttributeValue { Id = 17, Value = "5 lbs", ProductId = 4, ProductAttributeId = 5 },
                new ProductAttributeValue { Id = 18, Value = "10 lbs", ProductId = 4, ProductAttributeId = 5 },
                new ProductAttributeValue { Id = 19, Value = "Steel", ProductId = 4, ProductAttributeId = 6 },
                
                // CO2 Fire Extinguisher attribute values
                new ProductAttributeValue { Id = 20, Value = "CO2", ProductId = 5, ProductAttributeId = 4 },
                new ProductAttributeValue { Id = 21, Value = "10 lbs", ProductId = 5, ProductAttributeId = 5 },
                new ProductAttributeValue { Id = 22, Value = "Aluminum", ProductId = 5, ProductAttributeId = 6 },
                
                // Fire Blanket attribute values
                new ProductAttributeValue { Id = 23, Value = "N/A", ProductId = 6, ProductAttributeId = 4 },
                new ProductAttributeValue { Id = 24, Value = "4ft x 6ft", ProductId = 6, ProductAttributeId = 5 },
                new ProductAttributeValue { Id = 25, Value = "Fiberglass", ProductId = 6, ProductAttributeId = 6 },
                
                // Firefighter Jacket attribute values
                new ProductAttributeValue { Id = 26, Value = "Large", ProductId = 7, ProductAttributeId = 7 },
                new ProductAttributeValue { Id = 27, Value = "XL", ProductId = 7, ProductAttributeId = 7 },
                new ProductAttributeValue { Id = 28, Value = "Nomex", ProductId = 7, ProductAttributeId = 8 },
                new ProductAttributeValue { Id = 29, Value = "Yellow", ProductId = 7, ProductAttributeId = 9 },
                new ProductAttributeValue { Id = 30, Value = "Orange", ProductId = 7, ProductAttributeId = 9 },
                
                // Safety Helmet attribute values
                new ProductAttributeValue { Id = 31, Value = "Medium", ProductId = 8, ProductAttributeId = 7 },
                new ProductAttributeValue { Id = 32, Value = "Large", ProductId = 8, ProductAttributeId = 7 },
                new ProductAttributeValue { Id = 33, Value = "ABS Plastic", ProductId = 8, ProductAttributeId = 8 },
                new ProductAttributeValue { Id = 34, Value = "White", ProductId = 8, ProductAttributeId = 9 },
                new ProductAttributeValue { Id = 35, Value = "Yellow", ProductId = 8, ProductAttributeId = 9 }
            );
        }
    }
}
