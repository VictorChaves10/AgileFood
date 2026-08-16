using System.Security.Claims;
using AgileFood.Api.Auth;
using AgileFood.Application.Dtos.Consumptions;
using AgileFood.Application.Interfaces.Consumptions;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = TerminalApiKeyDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RegisterConsumption([FromBody] RegisterConsumptionDto dto)
    {
        if (dto is null)
            return BadRequest("O consumo e obrigatorio.");

        var result = await _consumptionService.RegisterConsumptionAsync(dto);
        return Ok(result);
    }

    [HttpGet("meus")]
    [Authorize]
    public async Task<IActionResult> GetMyConsumptions()
    {
        var result = await _consumptionService.GetByUserAsync(GetCurrentUserId());
        return Ok(result);
    }

    [HttpGet("meus/resumo")]
    [Authorize]
    public async Task<IActionResult> GetMyMonthlySummary()
    {
        var result = await _consumptionService.GetMonthlySummaryByUserAsync(GetCurrentUserId());
        return Ok(result);
    }

    private long GetCurrentUserId()
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.Parse(value!);
    }
}
