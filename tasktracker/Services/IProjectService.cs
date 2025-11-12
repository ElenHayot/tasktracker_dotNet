using tasktracker.DtoModels;
using tasktracker.Enums;

namespace tasktracker.Services
{
    /// <summary>
    /// Project service interface
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Get all projects with applied filter
        /// </summary>
        /// <param name="title">LIKE on title field</param>
        /// <param name="description">LIKE on description field</param>
        /// <param name="status">Filter on status</param>
        /// <returns>List of projects DTO</returns>
        Task<IEnumerable<ProjectDto>> GetAllProjectsFilteredAsync(string? title, string? description, StatusEnum? status);

        /// <summary>
        /// Get an existing project by its ID
        /// </summary>
        /// <param name="id">ID to find</param>
        /// <returns>Project DTO</returns>
        Task<ProjectDto> GetProjectByIdAsync(int id);

        /// <summary>
        /// Add a new project in DB
        /// </summary>
        /// <param name="project">Datas to create</param>
        /// <returns>The created project DTO</returns>
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto project);

        /// <summary>
        /// Update an existing project
        /// </summary>
        /// <param name="id">The project's ID to update</param>
        /// <param name="updatedProject">New datas</param>
        /// <returns>The updated project DTO</returns>
        Task<ProjectDto> UpdateProjectAsync(int id, UpdateProjectDto updatedProject);

        /// <summary>
        /// Delete an existing project
        /// </summary>
        /// <param name="id">The project's ID to delete</param>
        /// <returns>true/false</returns>
        Task<bool> DeleteProjectAsync(int id);
    }
}
