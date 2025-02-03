using MallMartDB.Models;
using MallMartAPI.Repos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IGenericRepository<Product> repo;
        private readonly IDBManager DbManager;

        public ProductsController(IGenericRepository<Product> repository, IDBManager manager)
        {
            repo = repository;
            this.DbManager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> items = new List<Product>();
            items = await DbManager.GetProductsOrderByCategory();
            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetAllFiltered([FromHeader(Name = "filterBy")] string filterBy)
        {
            List<Product> items = new List<Product>();

            if (filterBy == "AlmostOutOfStock")
                items = await DbManager.GetProductsAlmostOutOfStock();

            if (filterBy == "RatedHigh")
                items = await DbManager.GetProductsRatedHigh();

            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var item = await DbManager.GetProductById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product item)
        {
            var newItem = await repo.Insert(item);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id, controller = "products" }, newItem.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Product item)
        {
            bool updated = await repo.Update(id, item);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool deleted = await repo.Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
