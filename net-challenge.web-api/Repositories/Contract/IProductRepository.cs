using net_challenge.web_api.Models;

namespace net_challenge.web_api.Repositories.Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
