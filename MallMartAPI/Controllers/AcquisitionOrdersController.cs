﻿using MallMartDB.Models;
using MallMartAPI.Repos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcquisitionOrdersController : ControllerBase
    {
        private IGenericRepository<AcquisitionOrder> repo;
        public AcquisitionOrdersController(IGenericRepository<AcquisitionOrder> repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await repo.GetAll();
            if (items.Count() == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var item = await repo.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AcquisitionOrder item)
        {
            var newItem = await repo.Insert(item);
            return CreatedAtAction(nameof(GetById), new { id = newItem.AcquisitionOrderId, controller = "acquisitionOrders" }, newItem.AcquisitionOrderId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AcquisitionOrder item)
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
