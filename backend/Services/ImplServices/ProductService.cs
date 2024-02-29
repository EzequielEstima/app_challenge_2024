using backend.DTO.Product;
using backend.Exceptions;
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

    public async Task<ProductDTO?> CreateProduct(CreateProductDTO newProduct)
    {
        Product? product;
        try
        {
            product = await productRepo.createProduct(new Product {Nome = newProduct.Nome }); // ID is autoincremented by DB, so we don't need to set it here
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException)
        {
            throw new ProductAlreadyExistsException("Product already exists");
        }

        return ProductMapper.ToProductDTO(product);
    }

    public async Task<ProductDTO?> GetProductById(int id)
    {
        var product = await productRepo.getProductById(id);

        if(product == null) {
            throw new ProductNotFoundException("Product not found");
        }

        return ProductMapper.ToProductDTO(product);
    }

    public async Task<ListProductDTO> GetProducts()
    {
        return ProductMapper.ToListProductDTO(await productRepo.getProducts());
    }
}


