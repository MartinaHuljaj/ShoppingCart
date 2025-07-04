using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        public ProductController(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] int limit = 10, [FromQuery] int skip = 0)
        {
            var result = await _productService.GetProductsAsync(limit, skip);
            return Ok(result);
        }
        [HttpGet("products/{productId}")]
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
