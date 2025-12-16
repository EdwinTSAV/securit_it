using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordService _passwordService;

        public AuthService(ApplicationDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.UserName == username);

            //NO DEJAR
            //if (user != null)
            //{
            //    var plainPassword = user.PasswordHash; // contraseña vieja
            //    user.PasswordHash = _passwordService.HashPassword(user, plainPassword);
            //    await _context.SaveChangesAsync();
            //}
            // NO DEJAR

            if (user != null && _passwordService.VerifyPassword(user, password))
                return user;

            return null;
        }
    }
}
