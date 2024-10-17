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

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task UpdateProduct(int id, Product product)
        {
            await _productRepository.UpdateProduct(id, product);
        }
    }
}
