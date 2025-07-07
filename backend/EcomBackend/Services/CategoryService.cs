using EcomBackend.Data;
using EcomBackend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) => _db = db;

        public async Task<List<CategoryDTO>> GetCategoryTree()
        {
            var categories = await _db.Categories.ToListAsync();

            var lookup = categories.ToDictionary(c => c.Id, c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            });

            List<CategoryDTO> roots = [];

            foreach (var cat in categories)
            {
                if (cat.ParentId == null)
                {
                    roots.Add(lookup[cat.Id]);
                }
                else
                {
                    lookup[(int)cat.ParentId].SubCategories.Add(lookup[cat.Id]);
                }
            }

            return roots;
        }
    }
}