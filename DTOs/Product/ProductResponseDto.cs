using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int ProductTypeId { get; set; }
        public Dictionary<string, object> Attributes { get; set; }
    }
}
