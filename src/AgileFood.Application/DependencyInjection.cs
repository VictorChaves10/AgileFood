using AgileFood.Application.Interfaces.Auth;
using AgileFood.Application.Interfaces.Catalogs;
using AgileFood.Application.Interfaces.Consumptions;
using AgileFood.Application.Interfaces.ProductCategories;
using AgileFood.Application.Interfaces.Products;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Interfaces.Users;
using AgileFood.Application.Services.Auth;
using AgileFood.Application.Services.Catalogs;
using AgileFood.Application.Services.Consumptions;
using AgileFood.Application.Services.ProductCategories;
using AgileFood.Application.Services.Products;
using AgileFood.Application.Services.Stock;
using AgileFood.Application.Services.Users;
using AgileFood.Application.Validators.Users;
using AgileFood.Business.Models.Users;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AgileFood.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStockItemService, StockItemService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IConsumptionService, ConsumptionService>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

        services.TryAddSingleton(TimeProvider.System);
        services.TryAddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}
