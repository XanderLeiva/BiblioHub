using Microsoft.AspNetCore.Mvc;
using BiblioHub.DTOs;
using BiblioHub.Services;

namespace BiblioHub.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly AuthServices _authService;
    
    public AuthController(AuthServices authService) { 
        _authService = authService;
    }
    
    // POST /api/Auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var respuesta = await _authService.Register(dto);
            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return BadRequest(new  {message = ex.Message});
        }
    }

    // POST /api/Auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var respuesta = await _authService.Login(dto);
            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return BadRequest(new  {message = ex.Message});
        }
    }
}