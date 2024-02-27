using backend.DTO.Product;
using backend.Mappers;
using backend.Models;
using backend.Repos.IRepos;
using backend.Services.IServices;

namespace backend.Services.ImplServices;

public class ProductService : IProductService
{
    //private IProductRepo productRepo;

    static List<Product> products = new List<Product> {
        new Product { ProductId = 1, Nome = "Produto 1" },
        new Product { ProductId = 2, Nome = "Produto 2" },
        new Product { ProductId = 3, Nome = "Produto 3" },
        new Product { ProductId = 4, Nome = "Produto 4" },
        new Product { ProductId = 5, Nome = "Produto 5" }
    };

    static int nextId = 6;

    public ProductService(/*IProductRepo productRepo*/)
    {
        //this.productRepo = productRepo;
    }

    public Task<ProductDTO> CreateProduct(CreateProductDTO newProduct)
    {
        var product = new Product { ProductId = nextId++, Nome = newProduct.Nome };

        products.Add(product); //TODO call repo 
        
        return Task.FromResult(ProductMapper.ToProductDTO(product));
    }

    public Task<ProductDTO?> GetProductById(int id)
    {
        var product = products.FirstOrDefault(p => p.ProductId == id);// TODO call repo

        if(product == null) {
            return Task.FromResult<ProductDTO?>(null);
        }

        return Task.FromResult<ProductDTO?>(ProductMapper.ToProductDTO(product));
    }

    public Task<ListProductDTO> GetProducts()
    {
        return Task.FromResult(ProductMapper.ToListProductDTO(products));// TODO call repo
    }
}