using System.ComponentModel.DataAnnotations;

namespace BraveHeartBackend.DTOs.Product
{
    public class ProductCreateDTO
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "ProductTypeId is required.")]
        public int ProductTypeId { get; set; }
    }
}
