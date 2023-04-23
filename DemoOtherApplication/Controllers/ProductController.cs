using DemoOtherApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoOtherApplication.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly List<Product> _products;

    public ProductController(ILogger<ProductController> logger,List<Product> products)
    {
        _logger = logger;
        _products = products;
    }

    [HttpGet]
    [Route("get-all-product")]
    public List<Product> GetAllProduct()
    {
        return _products;
    }

    [HttpPost]
    [Route("add-product")]
    public ObjectResult AddProduct(Product product)
    {
        _products.Add(product);
        return new ObjectResult(product);
    }
}
