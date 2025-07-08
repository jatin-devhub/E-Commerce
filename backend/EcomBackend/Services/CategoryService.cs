using EcomBackend.DTOs;
using EcomBackend.Repositories;

namespace EcomBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<List<CategoryDTO>> GetCategoryTree()
        {
            var categories = await _repo.GetAll();

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
                    lookup[(int)cat.ParentId].Children.Add(lookup[cat.Id]);
                }
            }

            return roots;
        }
    }
}