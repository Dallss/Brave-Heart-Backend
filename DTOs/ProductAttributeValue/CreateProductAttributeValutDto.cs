using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BraveHeartBackend.DTOs.ProductAttributeValue
{
   public class ProductAttributeValueCreateDto
   {
      public int ProductAttributeId { get; set; }
      public string? Value { get; set; }
   }   
}