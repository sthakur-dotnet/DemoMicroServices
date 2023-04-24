
using InventoryServices.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton((IServiceProvider arg) => new List<Inventory> { new Inventory
{
    InventoryCount = 10,
    ProductId = 1,
} });
var app = builder.Build();

// Configure the HTTP request pipeline.
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
