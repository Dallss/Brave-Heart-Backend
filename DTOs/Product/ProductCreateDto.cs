using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductCreateDTO
    {
        [Required]
        public decimal? Price { get; set; }
        public int Stock { get; set; } = 0;

        [Required(ErrorMessage = "ProductTypeId is required.")]
        public int ProductTypeId { get; set; }

        public Dictionary<string, object> Attributes { get; set; } // Dynamic attributes
    }
}
