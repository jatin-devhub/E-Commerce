using EcomBackend.Data;
using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db) => _db = db;

        public async Task<List<CartItem>> GetByUser(int userId)
            => await _db.CartItems.Include(c => c.Product)
                                  .Where(c => c.UserId == userId)
                                  .ToListAsync();

        public async Task<CartItem?> GetByUserAndProduct(int userId, int productId)
            => await _db.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        public async Task Add(CartItem item)
        {
            await _db.CartItems.AddAsync(item);
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
