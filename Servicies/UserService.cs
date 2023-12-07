using System.Security.Cryptography;
using System.Text;
using UrlShortener_2_.Data.Interfaces;
using UrlShortener_2_.Data;
using UrlShortener_2_.Entities;
using Microsoft.EntityFrameworkCore;
using UrlShortener_2_.Models.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace UrlShortener_2_.Servicies
{
    public class UserService : IUserService
    {
        private readonly ShortenerDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ShortenerDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<User> GetUserById(int userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task ResetUrlShorts(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user != null)
            {
                // Reset the remaining short URL count
                user.RemainingShortUrls = 10; // or your desired initial value

                // Remove all existing URL mappings
                var urlMappings = _dbContext.NewUrls.Where(mapping => mapping.UserId == userId);
                _dbContext.NewUrls.RemoveRange(urlMappings);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

        }



        public string ObtainUserIdFromToken()
        {
            // Obtener el usuario actual desde el contexto HTTP
            //var user = _httpContextAccessor.HttpContext.User;

            //var userIdClaim = user.FindFirst("sub")?.Value;
            //string userIdClaim= System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue("sub");
            // Verificar si el usuario tiene un identificador (sub) en los claims del token
            // var userIdClaim = user.FindFirst("sub");

            var userIdClaim = "1";

            if (userIdClaim != null)
            {
                return userIdClaim;
            }

            // Devolver null si no se encuentra el identificador
            return null;
        }



    }
}

       