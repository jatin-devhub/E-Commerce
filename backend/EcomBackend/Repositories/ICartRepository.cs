using EcomBackend.Models;

namespace EcomBackend.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetByUser(int userId);
        Task<CartItem?> GetByUserAndProduct(int userId, int productId);
        Task Add(CartItem item);
        Task SaveChanges();
        Task<CartItem?> GetById(int id);
        Task Update(CartItem item);
        Task Delete(int id);
    }
}