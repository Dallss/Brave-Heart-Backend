using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BraveHeartBackend.Models
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // e.g., "ExtinguisherClass"

        [Required]
        public string DataType { get; set; } // e.g., "string", "int"

        public bool IsRequired { get; set; } // Indicates if this attribute is required for product creation
        
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
} 