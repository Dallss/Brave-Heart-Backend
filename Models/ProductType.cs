using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BraveHeartBackend.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "jsonb")]
        // Stores a JSON array of objects: [{ "name": "capacity", "isRequired": true, "dataType": "number", ... }]
        public string AttributesSchema { get; set; }
    }
}
