using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BraveHeartBackend.Models
{
    public class ProductAttributeValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; } // Store as string, convert as needed

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        [ForeignKey("ProductAttribute")]
        public int ProductAttributeId { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
    }
} 