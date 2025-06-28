using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.ProductAttribute
{
    public class ProductAttributeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsRequired { get; set; }
        public List<ProductAttributeValueResponseDto> Values { get; set; } = new List<ProductAttributeValueResponseDto>();
    }
}