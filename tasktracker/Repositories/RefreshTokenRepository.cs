using Microsoft.EntityFrameworkCore;
using tasktracker.Data;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// Refresh tokens repository
    /// </summary>
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        #region Instancies
        /// <summary>
        /// Local DB context instance
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Local common repository instance
        /// </summary>
        private readonly ICommonRepository _commonRepository;
        #endregion

        /// <summary>
        /// RefreshTokenRepository constructor
        /// </summary>
        /// <param name="context">DB context instance</param>
        /// <param name="commonRepository">Common repository instance</param>
        public RefreshTokenRepository(AppDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        /// <inheritdoc/>
        public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken)
        {
            var entry = await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async Task DeleteRefreshTokenAsync(int refreshTokenId)
        {
            _context.Remove(refreshTokenId);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RefreshToken>> GetAllRefreshTokensFilteredAsync(RefreshTokenQueryFilter filter)
        {
            IQueryable<RefreshToken> query = _context.RefreshTokens;
            query = _commonRepository.ApplyFilter(query, filter);
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
        {
            return await _context.RefreshTokens.Where(t => t.Token == token).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<RefreshToken> UpdateRefreshTokenAsync(RefreshToken refreshToken, RefreshToken updatedToken)
        {
            _context.RefreshTokens.Entry(refreshToken).CurrentValues.SetValues(updatedToken);
            await _context.SaveChangesAsync();
            return updatedToken;
        }

        /// <inheritdoc/>
        public async Task SaveUpdatesAsync(RefreshToken refreshToken)
        {
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RefreshToken>> GetUserRefreshTokensAsync(int userId)
        {
            return await _context.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
