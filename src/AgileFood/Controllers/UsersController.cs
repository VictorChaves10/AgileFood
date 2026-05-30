using AgileFood.Application.Dtos.Users;
using AgileFood.Application.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace AgileFood.Api.Controllers;

[Route("api/usuarios")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound("Usuario nao localizado.");

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        if (dto is null)
            return BadRequest("O usuario e obrigatorio.");

        var createdUser = await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUserDto dto)
    {
        if (dto is null || dto.Id != id)
            return BadRequest("Usuario invalido.");

        var updated = await _userService.UpdateAsync(dto);

        if (!updated)
            return NotFound("Usuario nao localizado.");

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
            return NotFound("Usuario nao localizado.");

        return NoContent();
    }
}
