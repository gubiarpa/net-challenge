using net_challenge.web_api.Models;

namespace net_challenge.web_api.Dtos
{
    #region Dtos
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class ProductCreateRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }
    }

    public class ProductCreateResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    #endregion


    #region Mappers
    public static class ProductMapperExtension
    {
        #region ToDto
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };
        }

        public static ProductCreateResponseDto ToCreateResponseDto(this Product product)
        {
            return new ProductCreateResponseDto()
            {
                Id = product.Id,
                Name = product.Name,
            };
        }
        #endregion

        #region ToModel
        public static Product ToModel(this ProductCreateRequestDto newProduct)
        {
            var now = DateTime.Now;

            return new Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                Stock = newProduct.Stock,
                CreatedBy = newProduct.UserId,
                CreatedDate = now,
                UpdatedBy = newProduct.UserId,
                UpdatedDate = now,
            };
        }
        #endregion
    }
    #endregion
}
