using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.Models
{
    public class FireExtinguisher : Product
    {
        // E.g., A, B, C, D, K
        [Required]
        [MaxLength(5)]
        public string ExtinguisherClass { get; set; }

        [Required]
        [MaxLength(20)]
        public string Size { get; set; } // e.g., "2kg", "5kg"
    }
} 