using EcomBackend.DTOs;

namespace EcomBackend.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategoryTree();
    }
}
