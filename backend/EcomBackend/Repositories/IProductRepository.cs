using EcomBackend.Models;

namespace EcomBackend.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product> GetProductById(int id);

        Task<List<Product>> GetByCategoryIds(List<int> categoryIds);
    }
}