using tasktracker.Enums;

namespace tasktracker.DtoModels
{
    /// <summary>
    /// DTO query filter model for User
    /// </summary>
    public class UserQueryFilter : BaseQueryFilter
    {
        public string? Name { get; set; }
        public string? Firstname { get; set; }
        public RolesEnum? Role { get; set; }
    }
}