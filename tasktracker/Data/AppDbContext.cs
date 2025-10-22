using Microsoft.EntityFrameworkCore;
using tasktracker.Entities;

namespace tasktracker.Data
{
    public class AppDbContext : DbContext
    {
        // Le constructeur reçoit la configuration
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // On déclare ici nos tables
        public DbSet<UserEntity> Users { get; set; }
    }
}
