using AgileFood.Application.Dtos.Stock;
using AgileFood.Application.Interfaces.Stock;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

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
        var stockItem = await _stockItemService.GetByIdAsync(id);

        if (stockItem is null)
            return NotFound("Estoque não localizado.");

        return Ok(stockItem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stockItens = await _stockItemService.GetAllAsync();
        return Ok(stockItens);
    }

    [HttpPost("{id:long}/entrada")]
    public async Task<IActionResult> RegisterStockEntry(long id, [FromBody] RegisterStockMovementDto entryDto)
    {
        if (entryDto == null)
            return BadRequest();

        var added = await _stockItemService.RegisterEntryAsync(id, entryDto);
        
        if (!added)
            return NotFound("Estoque não localizado");

        return NoContent();
    }

    [HttpPost("{id:long}/saida")]
    public async Task<IActionResult> RegisterStockExit(long id, [FromBody] RegisterStockMovementDto exitDto)
    {
        if (exitDto == null)
            return BadRequest();

        var added = await _stockItemService.RegisterExitAsync(id, exitDto);

        if (!added)
            return NotFound("Estoque não localizado");

        return NoContent();
    }
}
