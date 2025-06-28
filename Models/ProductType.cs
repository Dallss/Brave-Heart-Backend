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

        public ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
