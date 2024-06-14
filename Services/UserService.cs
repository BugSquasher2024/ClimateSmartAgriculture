using System;
using System.Linq;
using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ClimateSmartAgriculture.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Register(string name, string email, string password, string role)
        {
            if (_context.Users.Any(u => u.Email == email))
                return false;

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = HashPassword(password), // Use the updated HashPassword method
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.PasswordHash)) // Use the updated VerifyPassword method
                return null;

            return user;
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine the salt and hashed password together
            return $"{saltString}:{hashed}";
        }

        private bool VerifyPassword(string password, string storedHashedPassword)
        {
            // Split the stored hashed password into salt and hash
            var parts = storedHashedPassword.Split(':');
            if (parts.Length != 2)
                return false;

            string saltString = parts[0];
            string storedHash = parts[1];

            byte[] salt = Convert.FromBase64String(saltString);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == storedHash;
        }
    }
}
