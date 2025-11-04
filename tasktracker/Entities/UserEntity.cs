using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Entities
{
    /// <summary>
    /// Entity model for Users table
    /// </summary>
    public class UserEntity : HandlingDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Firstname { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string PasswordHash { get; set; }
        [Required]
        public required RolesEnum Role { get; set; }
    }
}
