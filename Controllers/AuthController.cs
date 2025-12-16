using ApiBackend.Models;
using ApiBackend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController (AuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                // Mejorar no funciona
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { errors = errores });
            }

            var user = await _authService.LoginAsync(request.UserName, request.Password);

            if (user == null)
                return Unauthorized(new { success = false, message = "Usuario o contraseña incorrectos." });

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                success = true,
                token,
                user = new
                {
                    user.UserId,
                    user.UserName,
                    user.Rol!.Nombre
                }
            });
        }
    }

    public class LoginRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public required string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string Password { get; set; } = string.Empty;
    }
}