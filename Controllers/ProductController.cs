using BraveHeartBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BraveHeartBackend.Data;
using BraveHeartBackend.DTOs.Product;
using BraveHeartBackend.DTOs.ProductAttribute;

namespace BraveHeartBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor
        public ProductController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
           _context = context;
           _userManager = userManager;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductType.Attributes)
                    .ThenInclude(attr => attr.Values)
                .ToListAsync();

            var result = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                Attributes = p.ProductType.Attributes.Select(attr => new ProductAttributeResponseDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired,
                    Values = attr.Values
                        .Where(v => v.ProductId == p.Id)
                        .Select(v => new ProductAttributeValueResponseDto
                        {
                            Id = v.Id,
                            Value = v.Value
                        }).ToList()
                }).ToList()
            });

            return Ok(result);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductType.Attributes)
                    .ThenInclude(attr => attr.Values)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            var result = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                Attributes = product.ProductType.Attributes.Select(attr => new ProductAttributeResponseDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired,
                    Values = attr.Values
                        .Where(v => v.ProductId == product.Id)
                        .Select(v => new ProductAttributeValueResponseDto
                        {
                            Id = v.Id,
                            Value = v.Value
                        }).ToList()
                }).ToList()
            };

            return Ok(result);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var productTypeExists = await _context.ProductTypes
                .AnyAsync(pt => pt.Id == dto.ProductTypeId);
            if (!productTypeExists)
                return BadRequest("Invalid ProductTypeId.");

            // Fetch required attributes for this product type
            var requiredAttributes = await _context.ProductAttributes
                .Where(a => a.ProductTypeId == dto.ProductTypeId && a.IsRequired)
                .ToListAsync();

            // Validate that all required attributes are present in the request
            var missingAttributes = requiredAttributes
                .Where(attr => dto.AttributeValues == null || !dto.AttributeValues.Any(av => av.ProductAttributeId == attr.Id && !string.IsNullOrWhiteSpace(av.Value)))
                .ToList();

            if (missingAttributes.Any())
            {
                var missingNames = string.Join(", ", missingAttributes.Select(a => a.Name));
                return BadRequest($"Missing required attribute values: {missingNames}");
            }

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                ProductTypeId = dto.ProductTypeId,
                ImageUrl = dto.ImageUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Save attribute values
            if (dto.AttributeValues != null)
            {
                foreach (var attrValue in dto.AttributeValues)
                {
                    // Only save if value is not null/empty
                    if (!string.IsNullOrWhiteSpace(attrValue.Value))
                    {
                        var pav = new ProductAttributeValue
                        {
                            ProductId = product.Id,
                            ProductAttributeId = attrValue.ProductAttributeId,
                            Value = attrValue.Value
                        };
                        _context.ProductAttributeValues.Add(pav);
                    }
                }
                await _context.SaveChangesAsync();
            }

            // Fetch the created product with all its data for response
            var createdProduct = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductType.Attributes)
                    .ThenInclude(attr => attr.Values)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (createdProduct == null)
                return NotFound();

            var response = new ProductResponseDto
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Stock = createdProduct.Stock,
                ImageUrl = createdProduct.ImageUrl,
                Attributes = createdProduct.ProductType.Attributes.Select(attr => new ProductAttributeResponseDto
                {
                    Id = attr.Id,
                    Name = attr.Name,
                    DataType = attr.DataType,
                    IsRequired = attr.IsRequired,
                    Values = attr.Values
                        .Where(v => v.ProductId == createdProduct.Id)
                        .Select(v => new ProductAttributeValueResponseDto
                        {
                            Id = v.Id,
                            Value = v.Value
                        }).ToList()
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDTO dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.ProductTypeId = dto.ProductTypeId;
            product.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BusinessOwner")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
