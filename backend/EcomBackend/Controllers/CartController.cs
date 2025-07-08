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

        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCart()
        {
            var cart = await _cartService.GetCart();
            return Ok(cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartItemDTO updateCartDTO)
        {
            try
            {
                await _cartService.UpdateCartItem(id, updateCartDTO);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await _cartService.DeleteCartItem(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}