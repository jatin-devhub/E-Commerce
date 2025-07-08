using EcomBackend.DTOs;
using EcomBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductDTO>>> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound();

            var dto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                AvailableQty = product.AvailabilityQty
            };
            return Ok(dto);
        }

        [HttpGet("{id}/related")]
        public async Task<ActionResult<List<ProductDTO>>> GetRelated(int id)
        {
            var related = await _productService.GetRelated(id);
            var dtos = related.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                AvailableQty = p.AvailabilityQty
            }).ToList();
            return Ok(dtos);
        }
    }
}