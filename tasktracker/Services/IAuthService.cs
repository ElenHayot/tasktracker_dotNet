using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Services
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Generate a Json Web Token for a user
        /// </summary>
        /// <param name="user">User to connect</param>
        /// <returns>A JWT</returns>
        string GenerateJwtToken(UserEntity user);


        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginDto">Login informations</param>
        /// <returns>Ok/Nok</returns>
        Task<(string Token, UserDto User)> LoginUser(UserLoginDto loginDto);
    }
}
