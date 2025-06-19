using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.Models
{
    public class CheckoutDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        // Foreign key to ApplicationUser
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
} 