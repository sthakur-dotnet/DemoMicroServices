using MediatR;
using ProductServices.Models;

namespace ProductServices.Query.Handler;

public class GetAllProductRequestHandler:IRequestHandler<GetAllProductQuery,List<Product>>
{
    private readonly List<Product> _products;

    public GetAllProductRequestHandler(List<Product> products)
    {
        _products = products;
    }
    public async Task<List<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        return _products;
    }
}