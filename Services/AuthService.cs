// namespace PizzaShop.Services;

// using PizzaShop.Models;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;
using PizzaShop.Services.Interfaces;
using PizzaShop.Utils;
// using PizzaShop.Utils;

namespace PizzaShop.Services
{
    public class AuthService : IAuthService
    {
        private readonly PizzashopContext _context;

        public AuthService(PizzashopContext context)
        {
            _context = context;
        }

        public async Task<Account?> AuthenticateUser(string email, string password)  //Account instead of User
        {
            var user = await _context.Accounts.Include(u => u.Role)
                                           .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !PasswordUtills.VerifyPassword(password, user.Password))
                return null;

            return user;
        }

        public async Task<Role?> CheckRole(string role)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
        }

        // public async Task<bool> RegisterUser(User user)
        // {
        //     if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        //         return false;

        //     user.Password = PasswordUtills.HashPassword(user.password);
        //     _context.Users.Add(user);
        //     await _context.SaveChangesAsync();

        //     return true;
        // }
    }
}
