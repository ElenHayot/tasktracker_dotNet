
using Microsoft.EntityFrameworkCore;
using tasktracker.Data;

namespace tasktracker.Services
{
    /// <summary>
    /// Service to clean up old tokens
    /// Run task every 24 hours
    /// </summary>
    public class TokenCleanupService : BackgroundService
    {
        /// <summary>
        /// Local service provider instance
        /// </summary>
        private readonly IServiceProvider _services;

        /// <summary>
        /// Local logger instance
        /// </summary>
        private readonly ILogger<TokenCleanupService> _logger;

        public TokenCleanupService(IServiceProvider services, ILogger<TokenCleanupService> logger)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Delete tokens expired or revoked 30 days ago
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Delete tokens exired 30 days ago
                var cutoffDate = DateTime.UtcNow.AddDays(30);
                var expiredTokens = await context.RefreshTokens.Where(rt => rt.ExpiresAt < cutoffDate || (rt.IsRevoked && rt.RevokedAt < cutoffDate)).ToListAsync(stoppingToken);

                context.RefreshTokens.RemoveRange(expiredTokens);
                await context.SaveChangesAsync(stoppingToken);

                _logger.LogInformation($"Cleaned-up {expiredTokens.Count} expired refresh tokens.");

                // Wait 24h before next run
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
