using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Application.Interfaces.Stock;
using Microsoft.AspNetCore.Mvc;

namespace AgiliFood.Api.Controllers;

[Route("api/estoques")]
[ApiController]
public class StockItemController : Controller
{
    private readonly IStockItemService _stockItemService;

    public StockItemController(IStockItemService stockItemService)
    {
        _stockItemService = stockItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockItemDto stockItemDto)
    {
        if (stockItemDto == null)
            return BadRequest();

        var createdStock = await _stockItemService.CreateAsync(stockItemDto);
        return CreatedAtAction(nameof(GetById), new { createdStock.Id }, createdStock);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var stock = await _stockItemService.GetByIdAsync(id);

        if (stock is null)
            return NotFound("Estoque não localizado.");

        return Ok(stock);
    }

    [HttpPost("{id:long}/entrada")]
    public async Task<IActionResult> AddStockEntry(long id, [FromBody] RegisterStockEntryDto entryDto)
    {
        if (entryDto == null)
            return BadRequest();

        var added = await _stockItemService.AddStockEntryAsync(id, entryDto);
        
        if (!added)
            return NotFound("Estoque não localizado");

        return NoContent();
    }
}
