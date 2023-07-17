using MallMartDB.Models;
using MallMartAPI.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IGenericRepository<Customer> repo;
        private readonly IDBManager dBManager;

        public CustomersController(IGenericRepository<Customer> repository, IDBManager dBManager)
        {
            repo = repository;
            this.dBManager = dBManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll()
        {
            //var items = await repo.GetAll();
            var items = await dBManager.GetCustomers();

            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            //var item = await repo.GetById(id);
            var item = await dBManager.GetCustomerById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer item)
        {
            var newItem = await repo.Insert(item);
            return CreatedAtAction(nameof(GetById), new { id = newItem.CustomerId, controller = "customers" }, newItem.CustomerId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Customer item)
        {
            bool updated = await repo.Update(id, item);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] Customer item)
        {
            bool updated = await dBManager.UpdateCustomerDetails(item);

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
