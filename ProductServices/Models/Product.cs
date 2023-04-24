namespace ProductServices.Models;

public class Product
{
    public int? Id { get; set; }
    public string? Name { get; init; }
    public string? Description { get; set; }
    public decimal Price { get; init; }
}
