using AgileFood.Application.Dtos.ProductCategories;
using AgileFood.Application.Interfaces.ProductCategories;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/categorias")]
[ApiController]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryService _service;

    public ProductCategoryController(IProductCategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category == null)
            return NotFound("Categoria não localizada.");

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCategoryDto categoryDto)
    {
        if (categoryDto == null)
            return BadRequest("A categoria é obrigatória.");

        var createdCategory = await _service.CreateAsync(categoryDto);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCategoryDto categoryDto)
    {
        if (categoryDto == null || categoryDto.Id != id)
            return BadRequest("Categoria inválida.");

        var updated = await _service.UpdateAsync(categoryDto);

        if (!updated)
            return NotFound("Categoria não localizada.");
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound("Categoria não localizada.");

        return NoContent();
    }

}
