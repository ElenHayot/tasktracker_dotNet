using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// RefreshTokens repository interface
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Get all tokens with optional filter applied
        /// </summary>
        /// <param name="filter">Filter to apply on the query</param>
        /// <returns>List of refresh tokens</returns>
        Task<IEnumerable<RefreshToken>> GetAllRefreshTokensFilteredAsync(RefreshTokenQueryFilter filter);

        /// <summary>
        /// Get a refresh token by its token value
        /// </summary>
        /// <param name="token">Token value to find</param>
        /// <returns>A refresh token</returns>
        Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);

        /// <summary>
        /// Get a user's refresh tokens list
        /// </summary>
        /// <param name="userId">Associated user ID</param>
        /// <returns>List of refresh tokens</returns>
        Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(int userId);

        /// <summary>
        /// Add a new refresh token in DB
        /// </summary>
        /// <param name="refreshToken">Token to create</param>
        /// <returns>The created token</returns>
        Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken);

        /// <summary>
        /// Update an existing refresh token in DB
        /// </summary>
        /// <param name="refreshToken">Token to update</param>
        /// <param name="updatedToken">Updated token</param>
        /// <returns>The updated token</returns>
        Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken, RefreshToken updatedToken);

        /// <summary>
        /// Delete an existing refresh token from DB
        /// </summary>
        /// <param name="refreshTokenId">The token's ID to delete</param>
        /// <returns></returns>
        Task DeleteRefreshTokenAsync(int refreshTokenId);

        /// <summary>
        /// Save changes when changes done in service
        /// </summary>
        /// <param name="refreshToken">Entity to update</param>
        /// <returns></returns>
        Task SaveUpdatesAsync(RefreshToken refreshToken);
    }
}
