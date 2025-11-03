using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Data;
using Microsoft.EntityFrameworkCore;

namespace tasktracker.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        // Loading context
        private readonly AppDbContext _context;
        // Calling CommonRepository
        private readonly ICommonRepository _commonRepository;

        public TaskRepository(AppDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        /// <inheritdoc/>
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
        {
            var entry = await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteTaskAsync(TaskEntity task)
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TaskEntity>> GetAllTasksFilteredAsync(TaskQueryFilter filter)
        {
            IQueryable<TaskEntity> query = _context.Tasks;
            query = _commonRepository.ApplyFilter(query, filter);
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<TaskEntity?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<TaskEntity> UpdateTaskAsync(TaskEntity existingTask, TaskEntity updatedTask)
        {
            _context.Tasks.Entry(existingTask).CurrentValues.SetValues(updatedTask);
            await _context.SaveChangesAsync();
            return updatedTask;
        }
    }
}
