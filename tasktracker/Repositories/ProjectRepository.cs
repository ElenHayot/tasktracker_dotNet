using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using tasktracker.Data;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// Project repository - manage Projects db connexion and functions
    /// </summary>
    public class ProjectRepository : IProjectRepository
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
        /// ProjectRepository constructor
        /// </summary>
        /// <param name="context">Db context instance</param>
        /// <param name="commonRepository">Common repository instance</param>
        public ProjectRepository(AppDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        /// <inheritdoc/>
        public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
        {
            var entry = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async Task DeleteProjectAsync(ProjectEntity project)
        {
            _context.Remove(project);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProjectEntity>> GetAllProjectsFilteredAsync(ProjectQueryFilter filter)
        {
            IQueryable<ProjectEntity> query = _context.Projects;
            query = _commonRepository.ApplyFilter(query, filter);
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<ProjectEntity> UpdateProjectAsync(ProjectEntity existingProject, ProjectEntity updatedProject)
        {
            _context.Entry(existingProject).CurrentValues.SetValues(updatedProject);
            await _context.SaveChangesAsync();
            return updatedProject;
        }

        /// <inheritdoc/>
        public async Task SaveUpdatesAsync(ProjectEntity project)
        {
            await _context.SaveChangesAsync();
        }
    }
}
