using AgileFood.Business.Models.Weights;
using System.ComponentModel.DataAnnotations;

namespace AgileFood.Application.Dtos.Products;

public record CreateProductDto(

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    string Name,

    [StringLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres.")]
    string? Description,

    string? Brand,

    string? Flavor,

    bool IsActive,

    [Required(ErrorMessage = "O peso é obrigatório.")]
    decimal WeightAmount,

    [Required(ErrorMessage = "A unidade de peso é obrigatória.")]
    WeightUnitEnum WeightUnit,

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    decimal Price,

    [Required(ErrorMessage = "O código de barras é obrigatório.")]
    [StringLength(50, ErrorMessage = "O código de barras deve ter no máximo 50 caracteres.")]
    string BarCode,

    [Required(ErrorMessage = "A categoria do produto é obrigatória.")]
    int ProductCategoryId,

    string? Image,

    string? ImageUpload
);
