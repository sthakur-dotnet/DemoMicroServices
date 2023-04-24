using MediatR;
using ProductServices.Models;

namespace ProductServices.Query;

public record GetAllProductQuery():IRequest<List<Product>>
{
    
}