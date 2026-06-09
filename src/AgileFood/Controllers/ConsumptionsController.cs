using AgileFood.Application.Dtos.Consumptions;
using AgileFood.Application.Interfaces.Consumptions;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/consumos")]
[ApiController]
public class ConsumptionsController : ControllerBase
{
    private readonly IConsumptionService _consumptionService;

    public ConsumptionsController(IConsumptionService consumptionService)
    {
        _consumptionService = consumptionService;
    }

    [HttpPost("consumo")]
    public async Task<IActionResult> RegisterConsumption([FromBody] RegisterConsumptionDto dto)
    {
        if (dto is null)
            return BadRequest("O consumo e obrigatorio.");

        var result = await _consumptionService.RegisterConsumptionAsync(dto);
        return Ok(result);
    }
}
