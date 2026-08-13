using AgileFood.Api.Auth;
using AgileFood.Application.Interfaces.Catalogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/catalogo")]
[ApiController]
[Authorize(AuthenticationSchemes = TerminalApiKeyDefaults.AuthenticationScheme)]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailable([FromQuery] string? search)
    {
        var items = string.IsNullOrWhiteSpace(search)
            ? await _catalogService.GetAvailableItemsAsync()
            : await _catalogService.SearchAsync(search);

        return Ok(items);
    }
}
