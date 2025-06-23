 namespace BraveHeartBackend.DTOs.ProductAttribute
{
    public class CreateProductAttributeDTO
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsRequired { get; set; }
    }
}