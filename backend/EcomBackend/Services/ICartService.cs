using EcomBackend.DTOs;

namespace EcomBackend.Services
{
    public interface ICartService
    {
        Task AddToCart(AddToCartDTO cartDTO);
        Task<CartDTO> GetCart();
    }
}
