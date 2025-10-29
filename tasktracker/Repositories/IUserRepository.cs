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
    }
}
