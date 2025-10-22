using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace tasktracker.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Firstname { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
