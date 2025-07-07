using EcomBackend.DTOs;
using EcomBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO cartDTO)
        {
            try
            {
                await _cartService.AddToCart(cartDTO);
                return Ok(new { message = "Product added to cart" });
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { error = knf.Message });
            }
        }
    }

}