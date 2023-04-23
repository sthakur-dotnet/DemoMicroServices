using DemoMicroServices.Model;
using DemoOtherApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMicroServices.Controllers;
[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{

    private readonly ILogger<InventoryController> _logger;
    private readonly List<Inventory> _inventories;

    public InventoryController(ILogger<InventoryController> logger,List<Inventory> inventories)
    {
        _logger = logger;
        _inventories = inventories;
    }

    [HttpGet]
    [Route("get-inventory/{productId:int}")]
    public ObjectResult Get(int productId)
    {
        var result = _inventories.SingleOrDefault(_ => _.ProductId == productId);
        return result == default ? new NotFoundObjectResult("product not found") : new ObjectResult(result);
    }

    [HttpPost]
    [Route("add-inventory")]
    public ObjectResult AddInventory(Inventory requestModel)
    {
        var product = _inventories.SingleOrDefault(_ => _.ProductId == requestModel.ProductId);
        if (product == default)
        {
            var inv = new Inventory { ProductId = requestModel.ProductId, InventoryCount = requestModel.InventoryCount };
            _inventories.Add(inv);
            return new OkObjectResult(inv);
        }
        _inventories.Remove(product);
        product.InventoryCount += requestModel.InventoryCount;
        _inventories.Add(product);
        return new OkObjectResult(product);
    }

    [HttpGet]
    [Route("get-all-inventory")]
    public async  Task<ObjectResult> GetInventory()
    {
        var client = new HttpClient();

        var response = new List<InventoryResponse>();
        var products =await client.GetFromJsonAsync<List<Product>>(new Uri("http://demootherapplication/product/get-all-product"));
        
        foreach (var inventory in _inventories)
        {
            response.Add(new InventoryResponse
            {
                productid = inventory.ProductId,
                total = inventory.InventoryCount,
                productname = products.FirstOrDefault(_ => _.Id == inventory.ProductId).Name
            });
        }
        return new ObjectResult(response);

    }
}
