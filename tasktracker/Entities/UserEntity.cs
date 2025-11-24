using Microsoft.EntityFrameworkCore;
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
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity : HandlingDto
    {
        /// <summary>
        /// Users tables PK
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Family name
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Firstname
        /// </summary>
        [Required]
        public required string Firstname { get; set; }

        /// <summary>
        /// User's email - unique in DB
        /// </summary>
        [Required]
        public required string Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string? Phone {  get; set; }

        /// <summary>
        /// User's password hash
        /// </summary>
        [Required]
        public required string PasswordHash { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        [Required]
        public required RolesEnum Role { get; set; }

        /// <summary>
        /// User's associated tasks (IDs)
        /// </summary>
        public List<int>? TaskIds { get; set; } = [];
    }
}
