using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using tasktracker.Data;
using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly ICommonRepository _commonRepository;

        public ProjectRepository(AppDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        public async Task<ProjectEntity> CreateProjectAsync(ProjectEntity project)
        {
            var entry = await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProjectAsync(ProjectEntity project)
        {
            _context.Remove(project);
            await _context.SaveChangesAsync();
            return true;
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
    }
}
