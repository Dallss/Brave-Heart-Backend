using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BraveHeartBackend.Data;
using BraveHeartBackend.Models;
using BraveHeartBackend.DTOs.Product;
using BraveHeartBackend.DTOs.ProductType;
using BraveHeartBackend.DTOs.ProductAttribute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<ProductTypeResponseDto>>> GetAll()
        {
            var types = await _context.ProductTypes
                .Include(pt => pt.Attributes)
                .ToListAsync();
            var result = types.Select(pt => new ProductTypeResponseDto
            {
                Id = pt.Id,
                Name = pt.Name,
                Attributes = pt.Attributes.Select(attr => new ProductTypeResponseDto.ProductAttributeDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired
                }).ToList()
            });
            return Ok(result);
        }

        // GET: api/ProductType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeResponseDto>> GetById(int id)
        {
            var pt = await _context.ProductTypes
                .Include(pt => pt.Attributes)
                .FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            var result = new ProductTypeResponseDto
            {
                Id = pt.Id,
                Name = pt.Name,
                Attributes = pt.Attributes.Select(attr => new ProductTypeResponseDto.ProductAttributeDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired
                }).ToList()
            };
            return Ok(result);
        }

        // POST: api/ProductType
        [HttpPost]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<ActionResult<ProductTypeResponseDto>> Create([FromBody] CreateProductTypeDTO dto)
        {
            var productType = new ProductType
            {
                Name = dto.Name,
                Attributes = dto.Attributes.Select(a => new ProductAttribute
                {
                    Name = a.Name,
                    DataType = a.DataType,
                    IsRequired = a.IsRequired
                }).ToList()
            };
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
            var result = new ProductTypeResponseDto
            {
                Id = productType.Id,
                Name = productType.Name,
                Attributes = productType.Attributes.Select(attr => new ProductTypeResponseDto.ProductAttributeDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired
                }).ToList()
            };
            return CreatedAtAction(nameof(GetById), new { id = productType.Id }, result);
        }

        // PUT: api/ProductType/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductTypeDTO dto)
        {
            var pt = await _context.ProductTypes.Include(pt => pt.Attributes).FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            pt.Name = dto.Name;
            // For simplicity, replace all attributes
            _context.ProductAttributes.RemoveRange(pt.Attributes);
            pt.Attributes = dto.Attributes.Select(a => new ProductAttribute
            {
                Name = a.Name,
                DataType = a.DataType
            }).ToList();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ProductType/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Delete(int id)
        {
            var pt = await _context.ProductTypes.Include(pt => pt.Attributes).FirstOrDefaultAsync(pt => pt.Id == id);
            if (pt == null) return NotFound();
            _context.ProductAttributes.RemoveRange(pt.Attributes);
            _context.ProductTypes.Remove(pt);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
