using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for User
    /// </summary>
    public class UserQueryFilter : BaseQueryFilter
    {
        /// <summary>
        /// Query on user name field
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Query on user firstname field
        /// </summary>
        public string? Firstname { get; set; }

        /// <summary>
        /// Query on user phone field
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Query on user role field
        /// </summary>
        public RolesEnum? Role { get; set; }
    }
}