using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Application.Interfaces.Stock;
using Microsoft.AspNetCore.Mvc;

namespace AgiliFood.Api.Controllers;

[Route("api/estoque")]
[ApiController]
public class StockController : Controller
{
    private readonly IStockItemService _stockItemService;

    public StockController(IStockItemService stockItemService)
    {
        _stockItemService = stockItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockItemDto stockItemDto)
    {
        if (stockItemDto == null)
            return BadRequest();

        var createdStock = await _stockItemService.CreateStockItem(stockItemDto);
        return CreatedAtAction(nameof(GetById), new {createdStock.Id}, createdStock);        
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var stock = await _stockItemService.GetById(id);

        if (stock is null)
            return NotFound("Estoque não localizado.");

        return Ok(stock);
    }
}
