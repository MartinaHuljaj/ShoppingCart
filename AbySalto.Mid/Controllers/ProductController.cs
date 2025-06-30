using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int limit = 10, [FromQuery] int skip = 0)
        {
            var result = await _productService.GetProductsAsync(limit, skip);
            return Ok(result);
        }
        [HttpGet("products/{productId}")]
        [Authorize]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);
            if (result == null)
            {
                return NotFound(new { Message = "Product not found." });
            }
            return Ok(result);
        }
    }
}
