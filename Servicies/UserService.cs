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
            return _dbContext.Users.FirstOrDefault(p => p.UserName == authRequestBody.userName && p.PasswordHash == authRequestBody.password);
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

        
    }
}

       