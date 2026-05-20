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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetMyCart()
        {
            int userId = GetUserId();

            var cart = _cartService.GetMyCart(userId);

            return Ok(cart);
        }

        [HttpPost("items")]
        public IActionResult AddItem(AddCartItemDto dto)
        {
            try
            {
                int userId = GetUserId();

                _cartService.AddItem(userId, dto);

                return Ok("Ürün sepete eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("items/{cartItemId}")]
        public IActionResult UpdateItem(int cartItemId, UpdateCartItemDto dto)
        {
            try
            {
                int userId = GetUserId();

                _cartService.UpdateItemQuantity(userId, cartItemId, dto);

                return Ok("Sepet güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("items/{cartItemId}")]
        public IActionResult RemoveItem(int cartItemId)
        {
            try
            {
                int userId = GetUserId();

                _cartService.RemoveItem(userId, cartItemId);

                return Ok("Ürün sepetten silindi.");
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
