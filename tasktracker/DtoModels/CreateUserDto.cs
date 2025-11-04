using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to create (or update) a user - no ID field
    /// </summary>
    public class CreateUserDto
    {
        [Required]
        [MaxLength(15)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public required string Firstname { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required RolesEnum Role { get; set; } = RolesEnum.Guest;

        [Required]
        public required string Password { get; set; } = string.Empty;
    }
}
