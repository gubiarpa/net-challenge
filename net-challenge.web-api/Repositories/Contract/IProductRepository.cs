using net_challenge.web_api.Models;

namespace net_challenge.web_api.Repositories.Contract
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task AddProduct(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
    }
}
