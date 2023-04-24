using MediatR;
using ProductServices.Models;

namespace ProductServices.Command;

public record SaveProductCommand(Product product) : IRequest<Product>
{
}