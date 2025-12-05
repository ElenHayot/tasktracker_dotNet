using System.Security.Cryptography;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Exceptions;
using tasktracker.Repositories;

namespace tasktracker.Services
{
    /// <summary>
    /// Refresh token service
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        #region Instancies
        /// <summary>
        /// Local refresh token repository instance
        /// </summary>
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion

        /// <summary>
        /// RefreshTokenService constructor
        /// </summary>
        /// <param name="refreshTokenRepository">Refresh token repository instance</param>
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        #region Public methods
        /// <inheritdoc/>
        public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId, string ipAddress)
        {
            RefreshToken refreshToken = new()
            {
                UserId = userId,
                Token = GenerateSecureToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                IsRevoked = false
            };
            RefreshToken result = await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            RefreshToken? result = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);
            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> IsValidAsync(string token)
        {
            RefreshToken? result = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);
            if (result != null)
            {
                return result.ExpiresAt > DateTime.UtcNow && !result.IsRevoked;
            }
            else
                throw new NotFoundException($"Token not found.");
        }

        /// <inheritdoc/>
        public async Task RevokeAllUserTokensAsync(int userId, string ipAddress = "", string reason = "")
        {
            IEnumerable<RefreshToken> userTokens = await _refreshTokenRepository.GetUserRefreshTokensAsync(userId);
            foreach (RefreshToken token in userTokens)
            {
                token.RevokedAt = DateTime.UtcNow;
                token.RevokedByIp = ipAddress;
                token.IsRevoked = true;
                token.RevokedReason = reason;

                await _refreshTokenRepository.SaveUpdatesAsync(token);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> RevokeAsync(string token, string ipAddress = "", string reason = "")
        {
            RefreshToken? tokenToRevoke = await _refreshTokenRepository.GetRefreshTokenByTokenAsync(token);
            if (tokenToRevoke == null)
                throw new NotFoundException($"Token not found");

            tokenToRevoke.RevokedAt = DateTime.UtcNow
;            tokenToRevoke.RevokedByIp = ipAddress;
            tokenToRevoke.IsRevoked = true;
            tokenToRevoke.RevokedReason = reason;

            await _refreshTokenRepository.SaveUpdatesAsync(tokenToRevoke);
            return true;
        }

        /// <inheritdoc/>
        public async Task<RefreshToken> RotateAsync(string oldToken, string ipAddress)
        {
            RefreshToken? oldRefreshToken = await GetByTokenAsync(oldToken);
            if (oldRefreshToken == null) throw new NotFoundException($"Invalid token");

            // Create the new token
            RefreshToken newRefreshToken = await GenerateRefreshTokenAsync(oldRefreshToken.UserId, ipAddress);

            // Revoke old token
            oldRefreshToken.RevokedAt = DateTime.UtcNow;
            oldRefreshToken.RevokedByIp = ipAddress;
            oldRefreshToken.IsRevoked = true;
            oldRefreshToken.RevokedReason = "Replaced by rotation";
            oldRefreshToken.ReplacedByToken = newRefreshToken.Token;

            await _refreshTokenRepository.SaveUpdatesAsync(oldRefreshToken);

            return newRefreshToken;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Generate a 64 bytes token value
        /// </summary>
        /// <returns>Token value</returns>
        private string GenerateSecureToken()
        {
            var randomBytes = new Byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        #endregion
    }
}
