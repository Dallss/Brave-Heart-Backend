using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BraveHeartBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        // List of cart items directly under the user
        public List<CartItem> CartItems { get; set; } = new();

        // List of checkout details
        public List<CheckoutDetails> CheckoutDetails { get; set; } = new();
    }
}
