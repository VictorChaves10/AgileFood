using AgileFood.Application.Interfaces.ProductCategories;
using AgileFood.Application.Interfaces.Products;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Services.ProductCategories;
using AgileFood.Application.Services.Products;
using AgileFood.Application.Services.Stock;
using AgileFood.Business.Interfaces;
using AgileFood.Data.Context;
using AgileFood.Data.Repository;
using AgileFood.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockItemService, StockItemService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IStockItemRepository, StockItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
