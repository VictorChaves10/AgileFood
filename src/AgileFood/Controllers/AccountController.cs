using System.Security.Claims;
using AgileFood.Application.Dtos.Users;
using AgileFood.Application.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/conta")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPut("senha")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        if (request is null)
            return BadRequest("A senha atual e a nova senha são obrigatórias.");

        var dto = new ChangePasswordDto(GetCurrentUserId(), request.CurrentPassword, request.NewPassword);
        var changed = await _userService.ChangePasswordAsync(dto);

        if (!changed)
            return NotFound("Usuario nao localizado.");

        return NoContent();
    }

    [HttpPut("pin")]
    public async Task<IActionResult> ChangeTransactionPin([FromBody] ChangeTransactionPinRequestDto request)
    {
        if (request is null)
            return BadRequest("O PIN atual e o novo PIN são obrigatórios.");

        var dto = new ChangeTransactionPinDto(GetCurrentUserId(), request.CurrentPin, request.NewPin);
        var changed = await _userService.ChangeTransactionPinAsync(dto);

        if (!changed)
            return NotFound("Usuario nao localizado.");

        return NoContent();
    }

    private long GetCurrentUserId()
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.Parse(value!);
    }
}
