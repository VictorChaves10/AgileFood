using System.Text;
using AgileFood.Api.Auth;
using AgileFood.Application.Configuration;
using AgileFood.Application.Interfaces.Auth;
using AgileFood.Application.Interfaces.Catalogs;
using AgileFood.Application.Interfaces.Consumptions;
using AgileFood.Application.Interfaces.Notifications;
using AgileFood.Application.Interfaces.ProductCategories;
using AgileFood.Application.Interfaces.Products;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Interfaces.Users;
using AgileFood.Application.Services.Auth;
using AgileFood.Application.Services.Catalogs;
using AgileFood.Application.Services.Consumptions;
using AgileFood.Application.Services.Notifications;
using AgileFood.Application.Services.ProductCategories;
using AgileFood.Application.Services.Products;
using AgileFood.Application.Services.Stock;
using AgileFood.Application.Services.Users;
using AgileFood.Application.Validators.Users;
using AgileFood.Business.Interfaces;
using AgileFood.Data.Context;
using AgileFood.Data.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Token JWT obtido em /api/auth/login. Informe no formato: Bearer {seu token}.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityDefinition(TerminalApiKeyDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "Chave do dispositivo do terminal físico.",
        Name = TerminalApiKeyDefaults.HeaderName,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        },
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = TerminalApiKeyDefaults.AuthenticationScheme } },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>() ?? new JwtSettings();

builder.Services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    })
    .AddScheme<AuthenticationSchemeOptions, TerminalApiKeyAuthenticationHandler>(
        TerminalApiKeyDefaults.AuthenticationScheme, null);

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Services
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockItemService, StockItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConsumptionService, ConsumptionService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            status = statusCode,
            title = statusCode == StatusCodes.Status500InternalServerError
                ? "Erro interno."
                : "Requisicao invalida.",
            detail = exception?.Message
        });
    });
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
