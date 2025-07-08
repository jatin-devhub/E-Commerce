using EcomBackend.DTOs;
using EcomBackend.Models;

namespace EcomBackend.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsByCategory(int categoryId);

        Task<Product?> GetById(int id);

        Task<List<Product>> GetRelated(int id);
    }
}
