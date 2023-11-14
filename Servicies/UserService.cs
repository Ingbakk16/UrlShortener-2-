using System.Security.Cryptography;
using System.Text;
using UrlShortener_2_.Data.Interfaces;
using UrlShortener_2_.Data;
using UrlShortener_2_.Entities;
using Microsoft.EntityFrameworkCore;
using UrlShortener_2_.Models.Dtos;


namespace UrlShortener_2_.Servicies
{
    public class UserService : IUserService
    {
        private readonly ShortenerDbContext _dbContext;

        public UserService(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            var user = _dbContext.Users.FirstOrDefault(p => p.UserName == authRequestBody.userName);

            if (user == null)
            {
                return null; // User not found
            }

            // Verify the password
            using (var hmac = new HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authRequestBody.password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        return null; // Password doesn't match
                    }
                }
            }

            return user; // Password matches
        }




        public async Task<bool> IsUsernameTaken(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return await Task.FromResult(Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))));
            }
        }
    }
}

       