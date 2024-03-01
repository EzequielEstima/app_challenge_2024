using backend.Models;

namespace backend.Repos.IRepos;

public interface IProductRepo
{
    Task<IEnumerable<Product>> getProducts();
    Task<Product?> getProductById(int id);
    Task<Product> createProduct(Product newProduct);

    
    Task createProducts(IEnumerable<Product> products);
    Task deleteProduct(int id);
}
