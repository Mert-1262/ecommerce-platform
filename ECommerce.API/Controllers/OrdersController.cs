using ECommerce.Business.Abstract;
using ECommerce.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CheckoutDto? checkout)
        {
            try
            {
                int userId = GetUserId();

                var order = _orderService.Checkout(userId, checkout);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetMyOrders()
        {
            int userId = GetUserId();

            var orders = _orderService.GetMyOrders(userId);

            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/all")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
        {
            try
            {
                _orderService.UpdateOrderStatus(id, dto);

                return Ok("Sipariş durumu güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                int userId = GetUserId();

                var order = _orderService.GetById(id, userId);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetUserId()
        {
            string? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.Parse(userIdClaim!);
        }
    }
}
