using AgiliFood.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgiliFood.Api.Controllers;

[Route("api/stock-item")]
[ApiController]
public class StockItemController : ControllerBase
{
    private readonly IStockItemService _service;

    public StockItemController(IStockItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stockItems = await _service.GetAllAsync();

        if (stockItems == null || !stockItems.Any())
            return NotFound("No stock items found.");

        return Ok(stockItems);
    }
}
