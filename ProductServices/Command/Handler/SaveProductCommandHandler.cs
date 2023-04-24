using MediatR;
using ProductServices.Models;

namespace ProductServices.Command.Handler;

public class SaveProductCommandHandler:IRequestHandler<SaveProductCommand,Product>
{
    private readonly List<Product> _products; 
    public SaveProductCommandHandler(List<Product> products)
    {
        _products = products;
    }
    public async Task<Product> Handle(SaveProductCommand request, CancellationToken cancellationToken)
    {
        var nextId = _products.Max(_ => _.Id);
        var product = request.product;
        product.Id = nextId + 1;
        _products.Add(product);
        return product;
    }
}