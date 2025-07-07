using EcomBackend.Data;
using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) => _db = db;

        public async Task<List<Category>> GetAll()
            => await _db.Categories.AsNoTracking().ToListAsync();
    }
}