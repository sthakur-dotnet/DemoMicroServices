using MediatR;
using ProductServices.Models;

namespace ProductServices.Query.Handler;


public record GetProductQueryById(int Id) : IRequest<Product>;
