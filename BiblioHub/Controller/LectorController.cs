using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiblioHub.Services;

namespace BiblioHub.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LectorController: ControllerBase
{
    private readonly LectorServices _lectorServices;

    public LectorController(LectorServices lectorService)
    {
        _lectorServices = lectorService;
    }

    // api/Lector
    [HttpGet]
    public async Task<IActionResult> GetMyProfile()
    {
        try
        {
            var userId = User.FindFirst("user_id")?.Value
                         ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var lector = await _lectorServices.GetMyProfile(userId);
            return Ok(lector);
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }

    // api/Lector/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var lector = await _lectorServices.GetById(id);
            return Ok(lector);
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
    }
}   