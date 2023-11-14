﻿using System.Threading.Tasks;
using UrlShortener_2_.Entities;
using UrlShortener_2_.Models.Dtos;

namespace UrlShortener_2_.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUsernameTaken(string username);
        Task<bool> IsEmailTaken(string email);
        Task CreateUser(User user);
        Task<string> HashPassword(string password); // Add this method
        User? ValidateUser(AuthenticationRequestDto authRequestBody);
    }
}
