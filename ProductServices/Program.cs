using System.Reflection;
using ProductServices.Models;

namespace ProductServices;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddSingleton((IServiceProvider arg) => new List<Product>
        {
            new Product
            {
                Id = 1,
                Description = "Test1",
                Name = "Test2",
                Price = 10,
            },
            new Product
            {
                Id = 2,
                Description = "Test1",
                Name = "Test2",
                Price = 20,
            }
        });
        builder.Services.AddMediatR(opts =>
        {
            opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
