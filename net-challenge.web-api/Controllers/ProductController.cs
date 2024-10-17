using Microsoft.AspNetCore.Mvc;
using net_challenge.web_api.Dtos;
using net_challenge.web_api.Services.Contract;

namespace net_challenge.web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = (await _productService.GetProducts())
                .Select(product => product.ToProductDto());

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string str)
        {
            return Created();
        }
    }
}
