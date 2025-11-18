using System.ComponentModel.DataAnnotations;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model for authentication
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// User email - string
        /// </summary>
        [Required]
        public required string Email { get; set; }

        /// <summary>
        /// User password - string
        /// </summary>
        [Required]
        public required string Password { get; set; }
    }
}
