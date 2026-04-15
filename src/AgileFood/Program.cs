using AgileFood.Application.Interfaces.Catalogs;
using AgileFood.Application.Interfaces.Consumptions;
using AgileFood.Application.Interfaces.ProductCategories;
using AgileFood.Application.Interfaces.Products;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Interfaces.Users;
using AgileFood.Application.Services.Catalogs;
using AgileFood.Application.Services.Consumptions;
using AgileFood.Application.Services.ProductCategories;
using AgileFood.Application.Services.Products;
using AgileFood.Application.Services.Stock;
using AgileFood.Application.Services.Users;
using AgileFood.Business.Interfaces;
using AgileFood.Data.Context;
using AgileFood.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockItemService, StockItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConsumptionService, ConsumptionService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
