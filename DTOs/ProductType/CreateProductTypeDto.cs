using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BraveHeartBackend.DTOs.ProductAttribute;

namespace BraveHeartBackend.DTOs.ProductType
{
    public class CreateProductTypeDTO
    {
        [Required]
        public string Name { get; set; }

        public List<CreateProductAttributeDTO> Attributes { get; set; } = new();
    }

    public class ProductTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductAttributeResponseDTO> Attributes { get; set; }
    }

    public class ProductAttributeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}
