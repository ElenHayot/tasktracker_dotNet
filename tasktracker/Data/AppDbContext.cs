using Microsoft.EntityFrameworkCore;
using tasktracker.Entities;

namespace tasktracker.Data
{
    /// <summary>
    /// Database context management
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// AppDbContext constructor
        /// </summary>
        /// <param name="options">Context configuration</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        /// <summary>
        /// Declare datatables
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
