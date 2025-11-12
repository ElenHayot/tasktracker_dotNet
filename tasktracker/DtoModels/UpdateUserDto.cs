using System.ComponentModel.DataAnnotations;
using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO model to update user
    /// </summary>
    public class UpdateUserDto : HandlingDto
    {
        /// <summary>
        /// User email - unique in DB
        /// </summary>
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        [Phone]
        public string? Phone { get; set; }

        /// <summary>
        /// User role - default "User"
        /// </summary>
        public RolesEnum? Role { get; set; }

        /// <summary>
        /// User password - hash stored in DB
        /// Once registered, set back as empty for security
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// User associated tasks (IDs)
        /// </summary>
        public List<int>? TaskIds { get; set; }
    }
}
