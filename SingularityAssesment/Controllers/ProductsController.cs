using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SingularityAssesment;
using SingularityAssesment.Data;

namespace SingularityAssesment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SingularityAssesmentContext _context;

        public ProductsController(SingularityAssesmentContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.Where(p => p.StatusDelete != true).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null || product.StatusDelete == true)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var p = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StatusDelete = false,
                StatusLock = false

            };
            _context.Product.Add(p); await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = p.Id }, p);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Id is not matched");
            }

            var p = await _context.Product.FindAsync(id);
            if (p.StatusDelete == true || p.StatusLock == true)
            {
                return BadRequest();
            }

            p.Name = product.Name;
            p.Description = product.Description;
            p.Price = product.Price;
            p.StatusDelete = product.StatusDelete;
            p.StatusLock = product.StatusLock;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FirstOrDefaultAsync(p => p.StatusDelete != true && p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            if (product.StatusLock == true)
            {
                return BadRequest();
            }

            product.StatusDelete = true;
            await _context.SaveChangesAsync();

            return product;
        }

        [HttpPost("LockProduct/{id}")]
        public async Task<ActionResult<Product>> LockProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null || product.StatusDelete == true)
            {
                return NotFound();
            }

            product.StatusLock = product.StatusLock != true;

            await _context.SaveChangesAsync();
            return product;
        }

        [HttpGet("GetTrashProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> GetTrashProduct()
        {
            return await _context.Product.Where(p => p.StatusDelete == true).ToListAsync();
        }

        [HttpPost("RecoverProduct/{id}")]
        public async Task<ActionResult<Product>> RecoverProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.StatusDelete = false;

            await _context.SaveChangesAsync();
            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
