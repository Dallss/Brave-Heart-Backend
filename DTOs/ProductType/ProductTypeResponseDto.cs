using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.ProductType
{
    public class ProductTypeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductAttributeDto> Attributes { get; set; }

        public class ProductAttributeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string DataType { get; set; }
            public bool IsRequired { get; set; }
        }
    }
} 