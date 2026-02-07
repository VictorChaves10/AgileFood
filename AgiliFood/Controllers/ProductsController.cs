using AgiliFood.Application.Dtos;
using AgiliFood.Application.Dtos.Products;
using AgiliFood.Application.Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace AgiliFood.Api.Controllers;

[Route("api/product")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IProductService _service;
    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound("Produto não localizado");

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
    {
        if (productDto == null)
            return BadRequest("O produto é obrigatório.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdProduct = await _service.CreateAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] ProductDto productDto)
    {
        if (productDto == null || productDto.Id != id)
            return BadRequest("Produto com id inválido");

        var updatedProduct = await _service.UpdateAsync(productDto);

        if (updatedProduct == null)
            return NotFound("Produto não localizado");

        return Ok(updatedProduct);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound("Produto não localizado");

        return NoContent();
    }
}
