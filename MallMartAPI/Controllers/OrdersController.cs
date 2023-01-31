using MallMartDB.Models;
using MallMartAPI.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MallMartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IGenericRepository<Order> repo;
        private readonly IDBManager dBManager;

        public OrdersController(IGenericRepository<Order> repository, IDBManager dBManager)
        {
            repo = repository;
            this.dBManager = dBManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int custId)
        {
            if (custId == 0)
            {
                var items = await repo.GetAll();
                if (items.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(items);
            }
            else
            {
                var items = await dBManager.GetOrdersByCustomerId(custId);
                if (items.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(items);
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var item = await dBManager.GetOrderById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order item)
        {
            string errorMessage = await ValidateOrder(item);
            if (errorMessage == "")
            {
                foreach (var line in item.OrderLines)
                {
                    line.Order = item;
                }

                item = UpdateTimeToLocalTimeZone(item);
                var newItem = await dBManager.SaveOrder(item);
                return CreatedAtAction(nameof(GetById), new { id = newItem.OrderId, controller = "orders" }, newItem.OrderId);
            }
            else
            {
                return BadRequest(errorMessage);
            }
        }

        Order UpdateTimeToLocalTimeZone(Order order)
        {
            order.ArrivalTime = null;

            DateTime date = order.DateOrdered == null ? DateTime.Now : (DateTime)order.DateOrdered;
            order.DateOrdered = TimeZone.CurrentTimeZone.ToLocalTime(date);

            date = order.DueTimeFirst == null ? DateTime.Now : (DateTime)order.DueTimeFirst;
            order.DueTimeFirst = TimeZone.CurrentTimeZone.ToLocalTime(date);

            date = order.DueTimeLast == null ? DateTime.Now : (DateTime)order.DueTimeLast;
            order.DueTimeLast = TimeZone.CurrentTimeZone.ToLocalTime(date);

            return order;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Order item)
        {
            bool updated = await repo.Update(id, item);

            if (updated)
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Asymmetric")] // Use the "Asymmetric" authentication scheme
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool deleted = await repo.Delete(id);

            if (deleted)
                return Ok();
            else
                return NotFound();
        }


        #region Validations
        private async Task<string> ValidateOrder(Order order)
        {
            string message = "";

            if (order.DateOrdered == null)
                message += "Please insert ordered date\n\n";

            if (order.DueTimeFirst == null)
                message += "Please state when do you want the order to be delievered\n\n";

            if (order.DueTimeLast == null)
                message += "Please state when do you want the order to be delievered\n\n";

            if (order.TotalPrice == null)
                message += "Order arrived without price\n\n";

            if (order.OrderLines.Count() == 0)
                message += "Order arrived without products\n\n";

            return message;
        }

        #endregion
    }
}
