using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<ProductAttribute> Attributes { get; set; } = new();
    }
} 