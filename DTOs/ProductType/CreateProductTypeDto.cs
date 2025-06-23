using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.DTOs.ProductType
{
    public class CreateProductTypeDTO
    {
        [Required]
        public string Name { get; set; }
        public string AttributesSchema { get; set; } // JSON array of objects: [{ "name": "capacity", "isRequired": true, "dataType": "number", ... }]
    }

    public class ProductTypeResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AttributesSchema { get; set; } // JSON schema as string
    }
}
