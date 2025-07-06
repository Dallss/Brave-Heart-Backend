using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BraveHeartBackend.DTOs.ProductAttributeValue;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public List<CreateProductAttributeValueDto>? AttributeValues { get; set; }
    }
} 