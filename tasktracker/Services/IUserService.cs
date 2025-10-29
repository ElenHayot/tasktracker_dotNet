using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersFilteredAsync(string? name, string? firstname, RolesEnum? role);

        Task<UserDto?> GetUserByEmailAsync(string email);

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
    }
}
