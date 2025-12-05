using tasktracker.Entities;

namespace tasktracker.Services
{
    /// <summary>
    /// Refresh token service interface
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Create a new refresh token
        /// </summary>
        /// <param name="userId">Token's associated user ID</param>
        /// <param name="ipAddress">IP address asking for a new refresh token</param>
        /// <returns>A new refresh token</returns>
        Task<RefreshToken> GenerateRefreshTokenAsync(int userId, string ipAddress);
        
        /// <summary>
        /// Get a refresh token by its token value
        /// </summary>
        /// <param name="token">Value to find</param>
        /// <returns>An existing token or null</returns>
        Task<RefreshToken?> GetByTokenAsync(string token);

        /// <summary>
        /// Indicates if a refresh token is valid or not
        /// </summary>
        /// <param name="token">Token to find</param>
        /// <returns>true/false</returns>
        Task<bool> IsValidAsync(string token);

        /// <summary>
        /// Revoke an existing refresh token
        /// </summary>
        /// <param name="token">Token value to revoke</param>
        /// <param name="ipAddress">IP address revoking the token</param>
        /// <param name="reason">Reason to revoke</param>
        /// <returns>true/false</returns>
        Task<bool> RevokeAsync(string token, string ipAddress, string reason);

        /// <summary>
        /// Revoke all refresh tokens associated to a user ID
        /// </summary>
        /// <param name="userId">Token associated user ID</param>
        /// <param name="ipAddress">IP address revoking tokens</param>
        /// <param name="reason">Reason to revoke</param>
        /// <returns></returns>
        Task RevokeAllUserTokensAsync(int userId, string ipAddress, string reason);
        
        /// <summary>
        /// Rotate refresh token for a user
        /// </summary>
        /// <param name="oldToken">Old token value</param>
        /// <param name="ipAddress">IP address asking for a rotation</param>
        /// <returns>An updated refresh token</returns>
        Task<RefreshToken> RotateAsync(string oldToken, string ipAddress);
    }
}
