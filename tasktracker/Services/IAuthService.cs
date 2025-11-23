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
        Task<LoginResponseDto> LoginUser(UserLoginDto loginDto);

        /// <summary>
        /// Get the current user by the id stored in token
        /// </summary>
        /// <param name="userId">User ID stored in the token</param>
        /// <returns>Current user or null</returns>
        Task<UserDto> GetCurrentUserAsync(string? userId);
    }
}
