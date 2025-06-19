using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BraveHeartBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property for the user's cart
        public Cart Cart { get; set; }

        // Navigation property for the user's checkout details
        public List<CheckoutDetails> CheckoutDetails { get; set; } = new();
    }
} 