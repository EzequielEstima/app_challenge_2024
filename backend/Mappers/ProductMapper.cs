using backend.DTO.Product;
using backend.Models;

namespace backend.Mappers
{
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Nome = product.Nome
            };
        }

        public static ListProductDTO ToListProductDTO(IEnumerable<Product> products)
        {
            return new ListProductDTO
            {
                Products = products.Select(ToProductDTO).ToList()
            };
        }
    }
}