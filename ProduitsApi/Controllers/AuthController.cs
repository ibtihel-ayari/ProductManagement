using Microsoft.AspNetCore.Mvc;
using ProduitsApi.DTOs;
using ProduitsApi.Services;

namespace ProduitsApi.Controllers;

[ApiController]
[Route("api/[controller]")]     // → /api/auth
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var resultat = await _authService.RegisterAsync(dto);
        if (resultat is null)
            return Conflict(new { message = "Cet email est déjà utilisé." });
        return Ok(resultat);
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var resultat = await _authService.LoginAsync(dto);
        if (resultat is null)
            return Unauthorized(new { message = "Email ou mot de passe incorrect." });
        return Ok(resultat);   // renvoie le token
    }
}