using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BraveHeartBackend.Data;
using BraveHeartBackend.Models;
using BraveHeartBackend.DTOs.ProductType;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace BraveHeartBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeResponseDTO>>> GetAll()
        {
            var types = await _context.ProductTypes.ToListAsync();
            var result = types.Select(pt => new ProductTypeResponseDTO
            {
                Id = pt.Id,
                Name = pt.Name,
                AttributesSchema = pt.AttributesSchema
            });
            return Ok(result);
        }

        // GET: api/ProductType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeResponseDTO>> GetById(int id)
        {
            var pt = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            var result = new ProductTypeResponseDTO
            {
                Id = pt.Id,
                Name = pt.Name,
                AttributesSchema = pt.AttributesSchema
            };
            return Ok(result);
        }

        // POST: api/ProductType
        [HttpPost]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<ActionResult<ProductTypeResponseDTO>> Create([FromBody] CreateProductTypeDTO dto)
        {
            var productType = new ProductType
            {
                Name = dto.Name,
                AttributesSchema = dto.AttributesSchema
            };
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
            var result = new ProductTypeResponseDTO
            {
                Id = productType.Id,
                Name = productType.Name,
                AttributesSchema = productType.AttributesSchema
            };
            return CreatedAtAction(nameof(GetById), new { id = productType.Id }, result);
        }

        // PUT: api/ProductType/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductTypeDTO dto)
        {
            var pt = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            pt.Name = dto.Name;
            pt.AttributesSchema = dto.AttributesSchema;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ProductType/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Delete(int id)
        {
            var pt = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            _context.ProductTypes.Remove(pt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
