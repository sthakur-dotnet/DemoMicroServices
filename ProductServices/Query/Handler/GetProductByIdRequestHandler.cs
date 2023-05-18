using MediatR;
using ProductServices.Models;

namespace ProductServices.Query.Handler;

public class GetProductByIdRequestHandler:IRequestHandler<GetProductQueryById,Product>
{
    private readonly List<Product> _products;

    public GetProductByIdRequestHandler(List<Product> products)
    {
        _products = products;
    }
    public async Task<Product> Handle(GetProductQueryById request, CancellationToken cancellationToken)
    {
        return _products.SingleOrDefault(_ => _.Id==request.Id);
    }
}