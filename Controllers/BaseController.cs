using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiBackend.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ApplicationDbContext _context;

        protected BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected User GetLoggedUser()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Usuario no autenticado");

            var userId = int.Parse(userIdClaim.Value);

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                throw new UnauthorizedAccessException("Usuario no encontrado");

            return user;
        }

        protected string GetUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }
}
