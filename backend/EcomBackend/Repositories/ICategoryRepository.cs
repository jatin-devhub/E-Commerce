using EcomBackend.Models;

namespace EcomBackend.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
    }
}