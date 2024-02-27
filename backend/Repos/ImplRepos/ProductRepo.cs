
using backend.Models;
using backend.Repos.IRepos;
using Microsoft.EntityFrameworkCore;

namespace backend.Repos.ImplRepos;

public class ProductRepo : IProductRepo
{

    private DomainDBContext dbContext;

    public ProductRepo(DomainDBContext context)
    {
        this.dbContext = context;
    }

    public async Task<Product> createProduct(Product newProduct)
    {
        dbContext.Products.Add(newProduct);
        await dbContext.SaveChangesAsync();

        return newProduct;
    }

    public async Task deleteProduct(int id)
    {
        var product = await dbContext.Products.FindAsync(id);

        if (product == null) return;

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();    
    }

    public async Task<Product?> getProductById(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> getProducts()
    {
        return await dbContext.Products.ToListAsync();
    }
}