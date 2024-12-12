using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TryApi.ProductData;
using TryApi.Products;

namespace TryApi.Controllers // Замените на ваше пространство имен
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string Name = null, decimal? Price = null, string Description = null, int? Id = null)
        {
            var products = await _context.Products
            .Where(p => (Name == null || p.Name == Name) && (Price == null || p.Price == Price) && (Description == null || p.Description == Description) && (Id == null || p.id == Id))
            .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return products;
        }



        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // GET: api/products/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _context.Products
            .Where(p => p.Name == name)
            .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return products;
        }


        // GET: api/products/{price}
        [HttpGet("{price}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByPrice(decimal price)
        {
            var products = await _context.Products
            .Where(p => p.Price == price)
            .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return products;
        }

        // GET: api/products/{description}
        [HttpGet("{description}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByDescription(string description)
        {
            var products = await _context.Products
            .Where(p => p.Description == description)
            .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return products;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
        }

        private object GetProduct()
        {
            throw new NotImplementedException();
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if (id!= product.id)
            {
                return BadRequest("Product ID does not match.");
            }
            // Получаем продукт из базы данных по id
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            // Обновляем продукт в базе данных
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}