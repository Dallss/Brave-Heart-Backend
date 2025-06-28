using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "ProductTypeId is required.")]
        public int ProductTypeId { get; set; }

        public List<ProductAttributeValueCreateDto> AttributeValues { get; set; }

        public string? ImageUrl { get; set; }

        public class ProductAttributeValueCreateDto
        {
            public int ProductAttributeId { get; set; }
            public string? Value { get; set; }
        }
    }
}
