using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model for User
    /// </summary>
    public class UserDto : HandlingDto
    {
        [Required]
        public required int Id { get; set; }

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
        public required RolesEnum Role {  get; set; } = RolesEnum.Guest;

        [Required]
        public required string Password { get; set; } = string.Empty;
    }
}
