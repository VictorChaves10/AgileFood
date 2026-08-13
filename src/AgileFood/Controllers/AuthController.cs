using AgileFood.Api.Auth;
using AgileFood.Application.Dtos.Auth;
using AgileFood.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (dto is null)
            return BadRequest("As credenciais são obrigatórias.");

        var user = await _authService.LoginAsync(dto);
        var token = _tokenService.GenerateToken(user);

        return Ok(new
        {
            token = token.Token,
            expiresAtUtc = token.ExpiresAtUtc,
            user
        });
    }
}
