using backend.DTO.Product;
using backend.Mappers;
using backend.Models;
using backend.Repos.IRepos;
using backend.Services.IServices;

namespace backend.Services.ImplServices;

public class ProductService : IProductService
{
    private IProductRepo productRepo;

    public ProductService(IProductRepo productRepo)
    {
        this.productRepo = productRepo;
    }

    public async Task<ProductDTO> CreateProduct(CreateProductDTO newProduct)
    {
        var product = await productRepo.createProduct(new Product {Nome = newProduct.Nome }); // ID is autoincremented by DB, so we don't need to set it here

        return ProductMapper.ToProductDTO(product);
    }

    public async Task<ProductDTO?> GetProductById(int id)
    {
        var product = await productRepo.getProductById(id);

        if(product == null) {
            return null;
        }

        return ProductMapper.ToProductDTO(product);
    }

    public async Task<ListProductDTO> GetProducts()
    {
        return ProductMapper.ToListProductDTO(await productRepo.getProducts());
    }
}