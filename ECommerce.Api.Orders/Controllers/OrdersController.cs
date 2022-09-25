using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            (bool isSuccess, IEnumerable<Models.Order> Orders, string errorMessage) = await ordersProvider.GetOrdersAsync(customerId);
            if (isSuccess)
            {
                return Ok(Orders);
            }

            return NotFound(errorMessage);
        }
    }
}