using EcomBackend.DTOs;
using EcomBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var categoryTree = await _categoryService.GetCategoryTree();
            return Ok(categoryTree);
        }
    }
}