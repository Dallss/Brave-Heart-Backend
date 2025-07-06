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

            // Cascade delete: Deleting a ProductType deletes its Products
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed ProductType for Fire Extinguisher
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Fire Extinguisher" }
            );

            // Seed ProductAttributes for Fire Extinguisher
            modelBuilder.Entity<ProductAttribute>().HasData(
                new ProductAttribute { Id = 1, Name = "Color band", DataType = "string", IsRequired = true, ProductTypeId = 1 },
                new ProductAttribute { Id = 2, Name = "Extinguishing agent", DataType = "string", IsRequired = true, ProductTypeId = 1 },
                new ProductAttribute { Id = 3, Name = "Use", DataType = "string", IsRequired = true, ProductTypeId = 1 }
            );

            // Seed Products (Fire Extinguisher types)
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Dry Chemical (ABC)", Price = 0m, Stock = 0, ProductTypeId = 1, ImageUrl = "https://res.cloudinary.com/braveheartimages/image/upload/v1751823422/dry-chemical_t0rp88.png" }, // <-- Paste Cloudinary URL here
                new Product { Id = 2, Name = "Carbon Dioxide (CO₂)", Price = 0m, Stock = 0, ProductTypeId = 1, ImageUrl = "https://res.cloudinary.com/braveheartimages/image/upload/v1751823357/c02_zlry7d.png" }, // <-- Paste Cloudinary URL here
                new Product { Id = 3, Name = "Foam", Price = 0m, Stock = 0, ProductTypeId = 1, ImageUrl = "https://res.cloudinary.com/braveheartimages/image/upload/v1751823434/foam_ixdviw.png" }, // <-- Paste Cloudinary URL here
                new Product { Id = 4, Name = "Halon Substitute", Price = 0m, Stock = 0, ProductTypeId = 1, ImageUrl = "https://res.cloudinary.com/braveheartimages/image/upload/v1751823365/halon_nowrpt.png" } // <-- Paste Cloudinary URL here
            );

            // Seed ProductAttributeValues for each product
            modelBuilder.Entity<ProductAttributeValue>().HasData(
                // Dry Chemical (ABC)
                new ProductAttributeValue { Id = 1, Value = "White band on a red body", ProductId = 1, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 2, Value = "Multipurpose dry chemical", ProductId = 1, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 3, Value = "Effective on Class A (solids like wood/paper), B (flammable liquids/gases), and C (electrical) fires", ProductId = 1, ProductAttributeId = 3 },

                // Carbon Dioxide (CO₂)
                new ProductAttributeValue { Id = 4, Value = "Black band on a red body", ProductId = 2, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 5, Value = "Carbon dioxide gas", ProductId = 2, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 6, Value = "Ideal for Class B (flammable liquids) and Class C (electrical) fires—leaves no residue, safe for equipment", ProductId = 2, ProductAttributeId = 3 },

                // Foam
                new ProductAttributeValue { Id = 7, Value = "Blue band on a red body", ProductId = 3, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 8, Value = "Aqueous film-forming foam", ProductId = 3, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 9, Value = "Suitable for Class A (solids) and B (liquids) fires—forms a foam blanket to suppress vapors", ProductId = 3, ProductAttributeId = 3 },

                // Halon Substitute
                new ProductAttributeValue { Id = 10, Value = "White band on a light green body", ProductId = 4, ProductAttributeId = 1 },
                new ProductAttributeValue { Id = 11, Value = "Halon-replacement gases (such as FM-200 or similar clean agents)", ProductId = 4, ProductAttributeId = 2 },
                new ProductAttributeValue { Id = 12, Value = "Designed for Class A, B, and C fires—non-corrosive and leaves minimal residue", ProductId = 4, ProductAttributeId = 3 }
            );
        }
    }
}
