using EcomBackend.Data;
using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<List<Product>> GetAll()
            => await _db.Products.Include(p => p.Category).AsNoTracking().ToListAsync();

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetByCategoryIds(List<int> categoryIds)
        {
            return await _db.Products
                .Where(p => categoryIds.Contains(p.CategoryId))
                .ToListAsync();
        }
    }
}