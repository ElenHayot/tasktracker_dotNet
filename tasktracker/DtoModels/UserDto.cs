using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model for User
    /// </summary>
    public class UserDto : HandlingDto
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Required]
        public required int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [MaxLength(15)]
        public required string Name { get; set; }

        /// <summary>
        /// User firstname
        /// </summary>
        [Required]
        [MaxLength(15)]
        public required string Firstname { get; set; }

        /// <summary>
        /// User email - unique in DB
        /// </summary>
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        [Phone]
        public string? Phone { get; set; }

        /// <summary>
        /// User role - default "User"
        /// </summary>
        [Required]
        public required RolesEnum Role {  get; set; } = RolesEnum.Guest;

        /// <summary>
        /// User password - hash stored in DB
        /// Once registered, set back as empty for security
        /// </summary>
        [Required]
        public required string Password { get; set; } = string.Empty;

        /// <summary>
        /// User associated tasks (IDs)
        /// </summary>
        public List<int>? TaskIds { get; set; } = [];
    }
}
