using net_challenge.web_api.Models;
using net_challenge.web_api.Repositories.Contract;
using net_challenge.web_api.Services.Contract;

namespace net_challenge.web_api.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProduct(Product product)
        {
            await _productRepository.AddProduct(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await _productRepository.DeleteProduct(product);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateProduct(product);
        }
    }
}
