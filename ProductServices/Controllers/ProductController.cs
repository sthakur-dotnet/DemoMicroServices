using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Command;
using ProductServices.Models;
using ProductServices.Query;

namespace ProductServices.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-all-product")]
    public Task<List<Product>> GetAllProduct()
    {
        return _mediator.Send(new GetAllProductQuery());
    }

    [HttpPost]
    [Route("add-product")]
    public Task<Product> AddProduct(Product product)
    {
        return _mediator.Send(new SaveProductCommand(product));
    }
}


