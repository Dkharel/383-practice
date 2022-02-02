using System.Net;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc;
using SP22.P02.Web.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>();

var product1 = new Product
{
    Id = 1,
    Name = "GTA 5",
    Description = "The newest entry in the Grand Theft Auto series.",
    Price = 49.99m,
    SalePrice = 29.99m
};
products.Add(product1);

var product2 = new Product
{
    Id = 2,
    Name = "Red Dead Redemption 2",
    Description = "The newest entry in the Red Dead Redemption series.",
    Price = 59.99m,
    SalePrice = null
};
products.Add(product2);

var product3 = new Product
{
    Id = 3,
    Name = "Sims 2",
    Description = "A simulation game made by EA.",
    Price = 19.99m,
    SalePrice = null
};
products.Add(product3);

app.MapGet("api/products", () => (products));

app.MapPost("api/products/create", (ProductDto productDto) =>
{
    if (String.IsNullOrEmpty(productDto.Name))
    {
        return Results.BadRequest("Name cannot be empty.");
    }

    if (String.IsNullOrEmpty(productDto.Description))
    {
        return Results.BadRequest("Description cannot be empty.");
    }

    if (productDto.Name.Length > 120)
    {
        return Results.BadRequest("Name must be less that 120 characters.");
    }

    if (productDto.Price <= 0)
    {
        return Results.BadRequest("Price must be greater than 0.");
    }

    if (productDto.SalePrice != null && productDto.SalePrice <= 0)
    {
        return Results.BadRequest("If a product is on sale the sale price must be greater than 0.");
    }

    var productToCreate = new Product
    {
        Id = ProductIdGenerator(),
        Name = productDto.Name,
        Description = productDto.Description,
        Price = productDto.Price,
        SalePrice = productDto.SalePrice,
    };

    products.Add(productToCreate);

    return Results.Created("api/products/create", productToCreate);
});



app.MapGet("api/products/sales", () => (products.Where(x => x.SalePrice != null)));

//app.MapGet("api/products/sales/{id}", () => { }

int ProductIdGenerator()
{
    var highestId = products.Any() ? products.Max(x => x.Id) : 0;

    return highestId + 1;
}

//("api/products/edit", (ProductEditDto productEditDto) =>


app.Run();


//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
// Hi 383 - this is added so we can test our web project automatically. More on that later
public partial class Program { }