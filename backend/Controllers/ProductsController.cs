using backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController: ControllerBase {

    List<Product> products = new List<Product> {
        new Product { ProductId = 1, Nome = "Produto 1" },
        new Product { ProductId = 2, Nome = "Produto 2" },
        new Product { ProductId = 3, Nome = "Produto 3" },
        new Product { ProductId = 4, Nome = "Produto 4" },
        new Product { ProductId = 5, Nome = "Produto 5" }
    };

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(){

        return products;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id) {
        
        var product = products.Find(p => p.ProductId == id);

        if(product == null) {
            return NotFound();
        }

        return product;
    }    
    
}