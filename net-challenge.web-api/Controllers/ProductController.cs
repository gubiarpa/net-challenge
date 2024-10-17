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
                .Select(product => product.ToDto());

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequestDto productDto)
        {
            var product = productDto.ToModel();
            await _productService.AddProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductUpdateRequestDto productDto)
        {
            var product = productDto.ToModel(id);
            await _productService.UpdateProduct(id, product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
