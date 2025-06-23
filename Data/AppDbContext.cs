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
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CheckoutDetails> CheckoutDetails { get; set; }
    }
}
