using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OS_and_BD_lab_2.Models;
using System.Security.Cryptography;
using System.Text;

namespace OS_and_BD_lab_2.Service
{
    public class UserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string login, string password)
        {
            var user = await GetUserByLoginAsync(login);
            if (user == null) return null;
            return user.Password.Equals(HashPassword(password)) ? user : null;
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            // Проверяем, существует ли пользователь с таким логином
            if (await _context.Users.AnyAsync(u => u.Login == user.Login))
            {
                return false; // Пользователь уже существует
            }

            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        // Метод для хэширования пароля
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

}
