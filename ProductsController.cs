using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryApi.ProductData;
using TryApi.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TryApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize] // Защищаем весь контроллер от неавторизованных пользователей
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Инъекция контекста базы данных через конструктор
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                // _logger.LogError(ex, "Error occurred while getting products.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                // _logger.LogError(ex, "Error occurred while getting product.");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Category = dto.Category,
                    Stock = dto.Stock
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                // _logger.LogError(ex, "Error occurred while creating product.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
