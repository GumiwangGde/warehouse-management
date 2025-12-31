using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Application.Interfaces.Services;

namespace WarehouseManagement.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    ///  Authentication staff login
    /// </summary>
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request.Email, request.Password);
        return Ok(new { Token = token });
    }

    /// <summary>
    ///  Authentication staff register
    /// </summary>
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RequestRegister request)
    {
        await _authService.RegisterAsync(request);
        return Ok(new { Message = "Staff success registered" });
    }
}