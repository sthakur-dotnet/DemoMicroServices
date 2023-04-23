using DemoOtherApplication.Models;

namespace DemoOtherApplication;

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
                Description = "Test",
                Name = "Test",
                Price = 1,
                ProductType=ProductType.Electrical
            },
            new Product
            {
                Id = 2,
                Description = "Test",
                Name = "Test",
                Price = 1,
                ProductType=ProductType.Electrical
            }
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        //app.UseHttpsRedirection();

        //app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
