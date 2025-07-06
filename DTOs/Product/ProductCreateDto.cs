using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using BraveHeartBackend.DTOs.ProductAttributeValue;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "ProductTypeId is required.")]
        public int ProductTypeId { get; set; }

        public string? ImageUrl { get; set; }
        
        public List<CreateProductAttributeValueDto> AttributeValues { get; set; }
    }
}
