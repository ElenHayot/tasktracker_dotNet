using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retourne la liste des users avec filtre appliqué
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetAllUsersFilteredAsync(UserQueryFilter filter);

        /// <summary>
        /// Retrouve un utilisateur par son ID
        /// </summary>
        /// <param name="id">PK de Users</param>
        /// <returns></returns>
        Task<UserEntity?> GetUserByIdAsync(int id);

        /// <summary>
        /// Retourne un utilisateur unique avec l'email
        /// </summary>
        /// <param name="email">Email de Users</param>
        /// <returns></returns>
        Task<UserEntity?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">UserEntity to create</param>
        /// <returns>The created user</returns>
        Task<UserEntity> CreateUserAsync(UserEntity user);

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="existingUser">Existing user in DB</param>
        /// <param name="updatedUser">Updated user with new values</param>
        /// <returns>The updatedUser</returns>
        Task<UserEntity> UpdateUserAsync(UserEntity existingUser, UserEntity updatedUser);

        /// <summary>
        /// Delete an existing user
        /// </summary>
        /// <param name="id">User's ID to delete</param>
        /// <returns>Returns nothing</returns>
        Task<bool> DeleteUserAsync(UserEntity id);
    }
}
