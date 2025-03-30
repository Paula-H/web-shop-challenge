using Domain.Dto.Create;
using Domain.Dto.View;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this.orderService = orderService;
            _logger = logger;
        }

        [HttpGet("GetOrdersByUserId")]
        //[Authorize]
        public async Task<IActionResult> GetOrdersByUserId([FromQuery] int userId)
        {
            _logger.LogInformation("Getting orders for user: {0}", userId);
            var orders = await orderService.GetOrdersByUserIdAsync(userId);
            if (orders.Count == 0)
            {
                return NoContent();
            }
            return Ok(orders);
        }

        [HttpPost("PlaceOrder")]
        //[Authorize]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto placeOrderDto)
        {
            _logger.LogInformation("Placing order");
            var order = await orderService.PlaceOrderAsync(placeOrderDto);
            return Ok(order);
        }

        [HttpPut("ConfirmOrder")]
        //[Authorize]
        public async Task<IActionResult> ConfirmOrder([FromQuery] int orderId)
        {
            _logger.LogInformation("Completing order: {0}", orderId);
            await orderService.ConfirmOrderAsync(orderId);
            return Ok();
        }

        [HttpPut("CancelOrder")]
        //[Authorize]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)
        {
            _logger.LogInformation("Cancelling order: {0}", orderId);
            await orderService.CancelOrderAsync(orderId);
            return Ok();
        }

        [HttpPut("UpdateOrder")]
        //[Authorize]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                _logger.LogInformation("Updating order: {0}", orderDto.Id);
                await orderService.UpdateOrderAsync(orderDto);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to update order: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        //[Authorize]
        public async Task<IActionResult> DeleteOrder([FromQuery] int orderId)
        {
            try 
            {
                _logger.LogInformation("Deleting order: {0}", orderId);
                await orderService.DeleteOrderAsync(orderId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to delete order: {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}
