using EcomBackend.Data;
using EcomBackend.DTOs;
using EcomBackend.Models;
using EcomBackend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcomBackend.Services
{
    public class ProductService
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IProductRepository _productRepo;

        public ProductService(ICategoryRepository catRepo, IProductRepository productRepo)
        {
            _catRepo = catRepo;
            _productRepo = productRepo;
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var allCategories = await _catRepo.GetAll();

            var subCategoryIds = GetDescendantCategoryIds(allCategories, categoryId);
            subCategoryIds.Add(categoryId);

            var products = await _productRepo.GetByCategoryIds(subCategoryIds);

            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        private List<int> GetDescendantCategoryIds(List<Category> allCategories, int parentId)
        {
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