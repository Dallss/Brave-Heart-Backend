using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductCreateDTO
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "ProductTypeId is required.")]
        public int ProductTypeId { get; set; }

        public List<ProductAttributeValueCreateDto> AttributeValues { get; set; }

        public class ProductAttributeValueCreateDto
        {
            public int ProductAttributeId { get; set; }
            public string? Value { get; set; }
        }
    }
}
