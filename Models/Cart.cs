using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        // Navigation property for items in the cart
        public List<CartItem> Items { get; set; } = new();

        // Foreign key to ApplicationUser
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
