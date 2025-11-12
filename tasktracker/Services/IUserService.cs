using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Services
{
    /// <summary>
    /// User service interface
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all users with applied filter
        /// </summary>
        /// <param name="name">Filter on field 'name'</param>
        /// <param name="firstname">Filter on field 'firstname'</param>
        /// <param name="phone">Filter on field 'phone'</param>
        /// <param name="role">Filter on field 'role'</param>
        /// <returns>List of users DTO</returns>
        Task<IEnumerable<UserDto>> GetAllUsersFilteredAsync(string? name, string? firstname, string? phone, RolesEnum? role);

        /// <summary>
        /// Get one user by its email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <returns>A user DTO</returns>
        Task<UserDto> GetUserByEmailAsync(string email);

        /// <summary>
        /// Get one user by its ID
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>A user TO</returns>
        Task<UserDto> GetUserByIdAsync(int id);

        /// <summary>
        /// Add a new user in DB
        /// </summary>
        /// <param name="userDto">User to add</param>
        /// <returns>The added user DTO</returns>
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);

        /// <summary>
        /// Update an existing user in DB
        /// </summary>
        /// <param name="id">User's id to update</param>
        /// <param name="updatedUserDto">The "new" user</param>
        /// <returns>The updated user</returns>
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updatedUserDto);

        /// <summary>
        /// Delete an existing user in DB
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>true/false</returns>
        Task<bool> DeleteUserAsync(int id);

    }
}
