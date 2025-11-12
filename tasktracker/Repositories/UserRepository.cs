using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using tasktracker.Data;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// User repository - manage Users db connexion and functions
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Local db context instance
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// Local common repository instance
        /// </summary>
        private readonly ICommonRepository _commonRepository;

        /// <summary>
        /// UserRepository constructor
        /// </summary>
        /// <param name="context">Db context instance</param>
        /// <param name="commonRepository">Common repository instance</param>
        public UserRepository(AppDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserEntity>> GetAllUsersFilteredAsync(UserQueryFilter filter)
        {
            IQueryable<UserEntity> query = _context.Users;
            // Applique le filtre à la query
            query = _commonRepository.ApplyFilter(query, filter);
            
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            var entry = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async Task<UserEntity> UpdateUserAsync(UserEntity existingUser, UserEntity updatedUser)
        {
            _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUserAsync(UserEntity user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task SaveUpdatesAsync(UserEntity user)
        {
            await _context.SaveChangesAsync();
        }
    }
}
