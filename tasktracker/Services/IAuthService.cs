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
        /// Generate a Json Web Token - access token for a user
        /// </summary>
        /// <param name="user">User to connect</param>
        /// <returns>Access token value</returns>
        string GenerateJwtToken(UserEntity user);

        /// <summary>
        /// Return the client IP address
        /// </summary>
        /// <param name="context">Current http context</param>
        /// <returns>IP address</returns>
        string GetClientIp(HttpContext context);


        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginDto">Login informations</param>
        /// <param name="ipAddress">Login IP</param>
        /// <returns>A LoginResponseDto object</returns>
        Task<LoginServiceResponseDto> LoginUserAsync(UserLoginDto loginDto, string ipAddress);

        /// <summary>
        /// Get the current user by the id stored in token
        /// </summary>
        /// <param name="userId">User ID stored in the token</param>
        /// <returns>Current user or null</returns>
        Task<UserDto> GetCurrentUserAsync(string? userId);

        /// <summary>
        /// Refresh the access token for a valid refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token received</param>
        /// <returns>A LoginResponseDto object</returns>
        Task<LoginResponseDto> RefreshAsync(string refreshToken);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="refreshToken">Refresh token value to revoke</param>
        /// <param name="ipAddress">Client IP address</param>
        /// <returns></returns>
        Task LogoutUserAsync(string refreshToken, string ipAddress);

        /// <summary>
        /// Logout user from all devices
        /// </summary>
        /// <param name="userId">User ID to disconnect</param>
        /// <param name="ipAddress">Client IP address</param>
        /// <returns></returns>
        Task LogoutFromAllDevicesAsync(int userId, string ipAddress);
    }
}
