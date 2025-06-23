using BraveHeartBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BraveHeartBackend.Data;
using BraveHeartBackend.DTOs.Product;

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
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (!await IsBusinessOwner(currentUser))
                return Forbid();


            var productTypeExists = await _context.ProductTypes
                .AnyAsync(pt => pt.Id == dto.ProductTypeId);
            if (!productTypeExists)
                return BadRequest("Invalid ProductTypeId.");

            var product = new Product
            {
                Price = dto.Price,
                Stock = dto.Stock,
                ProductTypeId = dto.ProductTypeId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await IsBusinessOwner(currentUser))
                return Forbid();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;
            product.ProductTypeId = updatedProduct.ProductTypeId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await IsBusinessOwner(currentUser))
                return Forbid();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Helper: Checks if user has UserType == "BusinessOwner"
        private async Task<bool> IsBusinessOwner(IdentityUser user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            var userTypeProperty = dbUser?.GetType().GetProperty("UserType");
            var userTypeValue = userTypeProperty?.GetValue(dbUser)?.ToString();
            return userTypeValue == "BusinessOwner";
        }
    }
}
