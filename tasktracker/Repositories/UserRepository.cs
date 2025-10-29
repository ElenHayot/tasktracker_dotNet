using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using tasktracker.Data;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ICommonRepository _commonRepository;

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
    }
}
