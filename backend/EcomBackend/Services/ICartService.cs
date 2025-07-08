using EcomBackend.DTOs;

namespace EcomBackend.Services
{
    public interface ICartService
    {
        Task AddToCart(AddToCartDTO cartDTO);
        Task<CartDTO> GetCart();
        Task UpdateCartItem(int id, UpdateCartItemDTO cartUpdateDTO);
        Task DeleteCartItem(int id);
    }
}
