using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Project controller - manage Projects part
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]  // api/v1/projects
    public class ProjectsController : ControllerBase
    {
        /// <summary>
        /// Local project service instance
        /// </summary>
        IProjectService _projectService;

        /// <summary>
        /// ProjectController constructor
        /// </summary>
        /// <param name="service">Project service instance</param>
        public ProjectsController(IProjectService service)
        {
            _projectService = service;
        }

        /// <summary>
        /// Call Service.GetAllProjectsFilteredAsync
        /// </summary>
        /// <param name="title">FromQuery parameter 'title' - string</param>
        /// <param name="description">FromQuery parameter 'description' - string</param>
        /// <param name="status">FromQuery parameter 'status' - Enum value</param>
        /// <returns>List of projects</returns>
        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetAllProjectsFiltered([FromQuery] string? title, [FromQuery] string? description, [FromQuery] StatusEnum? status)
        {
            return await _projectService.GetAllProjectsFilteredAsync(title, description, status);
        }

        /// <summary>
        /// Call Service.GetProjectByIdAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>One project</returns>
        [HttpGet("{id}")]
        public async Task<ProjectDto> GetProjectById(int id)
        {
            return await _projectService.GetProjectByIdAsync(id);
        }

        /// <summary>
        /// Call Service.CreateProjectAsync
        /// </summary>
        /// <param name="dto">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var project = await _projectService.CreateProjectAsync(dto);
            return Ok(project);
        }

        /// <summary>
        /// Call Service.UpdateProjectAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <param name="dto">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(id, dto);
            return Ok(updatedProject);
        }

        /// <summary>
        /// Call Service.DeleteProjectAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>Ok/Nok result</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            bool deleted = await _projectService.DeleteProjectAsync(id);
            return Ok(deleted);
        }
    }
}
