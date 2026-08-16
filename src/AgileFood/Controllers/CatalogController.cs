using AgileFood.Api.Auth;
using AgileFood.Application.Dtos.Catalogs;
using AgileFood.Application.Interfaces.Catalogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/catalogo")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = TerminalApiKeyDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAvailable([FromQuery] string? search)
    {
        var items = string.IsNullOrWhiteSpace(search)
            ? await _catalogService.GetAvailableItemsAsync()
            : await _catalogService.SearchAsync(search);

        return Ok(items);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCatalogItemDto dto)
    {
        if (dto is null)
            return BadRequest("O item de catálogo é obrigatório.");

        var created = await _catalogService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAvailable), created);
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateCatalogItemDto dto)
    {
        if (dto is null || dto.Id != id)
            return BadRequest("Item de catálogo inválido.");

        var updated = await _catalogService.UpdateAsync(dto);

        if (!updated)
            return NotFound("Item de catálogo não localizado.");

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _catalogService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Item de catálogo não localizado.");

        return NoContent();
    }
}
