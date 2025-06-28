using System.Collections.Generic;
using BraveHeartBackend.DTOs.Product;

namespace BraveHeartBackend.DTOs.ProductType
{
    public class ProductTypeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductResponseDto> Products { get; set; } = new List<ProductResponseDto>();
    }
} 