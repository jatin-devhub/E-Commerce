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

        public async Task<CartItem?> GetById(int id)
        {
            return await _db.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Update(CartItem item)
        {
            _db.CartItems.Update(item);
            await SaveChanges();
        }

        public async Task Delete(int id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
            }
        }
    }
}
