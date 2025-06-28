using System.Collections.Generic;
using BraveHeartBackend.DTOs.ProductAttribute;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public List<ProductAttributeResponseDto> Attributes { get; set; } = new List<ProductAttributeResponseDto>();
    }
}
