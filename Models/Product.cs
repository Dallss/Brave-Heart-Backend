using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel; 

namespace BraveHeartBackend.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public string? ImageUrl { get; set; }
    }
}