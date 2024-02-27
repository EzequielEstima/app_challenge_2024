using backend.DTO.Product;
using backend.Models;
using backend.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController: ControllerBase {

    private IProductService productService;

    public ProductsController(IProductService productService) {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ListProductDTO>> GetProducts(){

        return await productService.GetProducts();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id) {
        
        var product = await productService.GetProductById(id);

        if(product == null) {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO newProduct) {
        var product = await productService.CreateProduct(newProduct);

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }
    
}