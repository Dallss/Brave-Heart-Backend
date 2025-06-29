using System.Collections.Generic;
using BraveHeartBackend.DTOs.Product;
using BraveHeartBackend.DTOs.ProductAttribute;

namespace BraveHeartBackend.DTOs.ProductType
{
    public class ProductTypeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
        public List<ProductAttributeResponseDto> Attributes { get; set; } = new List<ProductAttributeResponseDto>();
    }
} 