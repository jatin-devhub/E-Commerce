using EcomBackend.Data;
using EcomBackend.DTOs;
using EcomBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Services
{
    public class ProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var allCategories = await _db.Categories.ToListAsync();

            var subCategoryIds = GetDescendantCategoryIds(allCategories, categoryId);
            subCategoryIds.Add(categoryId);

            var products = await _db.Products
                .Where(p => subCategoryIds.Contains(p.CategoryId))
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();

            return products;
        }

        private List<int> GetDescendantCategoryIds(List<Category> allCategories, int parentId) {
            List<int> result = [];

            foreach (var cat in allCategories.Where(c => c.ParentId == parentId))
            {
                result.Add(cat.Id);
                result.AddRange(GetDescendantCategoryIds(allCategories, cat.Id));
            }

            return result;
        }
    }
}