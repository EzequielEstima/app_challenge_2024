using backend.DTO;
using backend.DTO.Product;


namespace backend.Services.IServices
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProduct(CreateProductDTO newProduct);
        Task<ListProductDTO> GetProducts();
        Task<ProductDTO?> GetProductById(int id);
    }
}