using net_challenge.web_api.Models;
using net_challenge.web_api.Repositories.Base;
using net_challenge.web_api.Repositories.Contract;

namespace net_challenge.web_api.Repositories.Implementation
{
    public class ProductRepository : SQLBase, IProductRepository
    {
        const string PRODUCT_ADD_SPNAME = "[dbo].[Product__Add]";
        const string PRODUCT_READ_SPNAME = "[dbo].[Product__Read]";
        const string PRODUCT_UPDATE_SPNAME = "[dbo].[Product__Update]";
        const string PRODUCT_DELETE_SPNAME = "[dbo].[Product__Delete]";

        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            AddParameter("@Name", product.Name);
            AddParameter("@Price", product.Price);
            AddParameter("@Stock", product.Stock);
            AddParameter("@UserId", product.CreatedBy);
            AddParameter("@ActionDate", product.CreatedDate);

            await ExecuteSPWithoutResults(PRODUCT_ADD_SPNAME);
        }

        public async Task DeleteProduct(int id)
        {
            AddParameter("id", id);

            await ExecuteSPWithoutResults(PRODUCT_DELETE_SPNAME);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await ExecuteSPWithResults(
                PRODUCT_READ_SPNAME,
                (reader) => new Product()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                    CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                    UpdatedBy = reader.GetInt32(reader.GetOrdinal("UpdatedBy")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                });
            return products;
        }

        public async Task UpdateProduct(int id, Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            AddParameter("@Id", id);
            AddParameter("@Name", product.Name);
            AddParameter("@Price", product.Price);
            AddParameter("@Stock", product.Stock);
            AddParameter("@UserId", product.CreatedBy);
            AddParameter("@ActionDate", product.CreatedDate);

            await ExecuteSPWithoutResults(PRODUCT_UPDATE_SPNAME);
        }
    }
}
