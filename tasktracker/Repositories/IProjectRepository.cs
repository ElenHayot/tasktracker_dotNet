using tasktracker.DtoModels;
using tasktracker.Entities;

namespace tasktracker.Repositories
{
    /// <summary>
    /// Project repository interface
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all projects with optional filter applied
        /// </summary>
        /// <param name="filter">Filter to apply on the query</param>
        /// <returns>Filtered project list</returns>
        Task<IEnumerable<ProjectEntity>> GetAllProjectsFilteredAsync(ProjectQueryFilter filter);

        /// <summary>
        /// Get one project by its ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>One project (entity)</returns>
        Task<ProjectEntity?> GetProjectByIdAsync(int id);

        /// <summary>
        /// Add a new project in DB
        /// </summary>
        /// <param name="project">Data to create</param>
        /// <returns>The created project</returns>
        Task<ProjectEntity> CreateProjectAsync(ProjectEntity project);
        
        /// <summary>
        /// Update an existing project
        /// </summary>
        /// <param name="existingProject">Project to update</param>
        /// <param name="updatedProject">New data</param>
        /// <returns>The updated project (entity)</returns>
        Task<ProjectEntity> UpdateProjectAsync(ProjectEntity existingProject, ProjectEntity updatedProject);

        /// <summary>
        /// Delete an existing project
        /// </summary>
        /// <param name="project">Project to delete</param>
        /// <returns></returns>
        Task DeleteProjectAsync(ProjectEntity project);

        /// <summary>
        /// Save changes when changes done in service
        /// </summary>
        /// <param name="project">Project to update</param>
        /// <returns>Nothing</returns>
        Task SaveUpdatesAsync(ProjectEntity project);
    }
}
