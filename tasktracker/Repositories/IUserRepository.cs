using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gett all users with applied filter
        /// </summary>
        /// <returns>List of users (entities)</returns>
        Task<IEnumerable<UserEntity>> GetAllUsersFilteredAsync(UserQueryFilter filter);

        /// <summary>
        /// Get a user by its ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>One user (entity)</returns>
        Task<UserEntity?> GetUserByIdAsync(int id);

        /// <summary>
        /// Get a user by its email
        /// </summary>
        /// <param name="email">Email to find</param>
        /// <returns>One user (entity)</returns>
        Task<UserEntity?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Add a user in DB
        /// </summary>
        /// <param name="user">UserEntity to create</param>
        /// <returns>The created user (entity)</returns>
        Task<UserEntity> CreateUserAsync(UserEntity user);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="existingUser">Existing user in DB</param>
        /// <param name="updatedUser">Updated user with new values</param>
        /// <returns>The updated user (entity)</returns>
        Task<UserEntity> UpdateUserAsync(UserEntity existingUser, UserEntity updatedUser);

        /// <summary>
        /// Delete an existing user
        /// </summary>
        /// <param name="user">User to delete</param>
        /// <returns>true/false</returns>
        Task<bool> DeleteUserAsync(UserEntity user);
    }
}
