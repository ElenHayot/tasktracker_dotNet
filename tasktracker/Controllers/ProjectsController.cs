using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Controller de la partie Projects
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]  // api/v1/projects
    public class ProjectsController : ControllerBase
    {
        IProjectService _projectService;
        public ProjectsController(IProjectService service)
        {
            _projectService = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetAllProjectsFiltered([FromQuery] string? title, [FromQuery] string? description, [FromQuery] StatusEnum? status)
        {
            return await _projectService.GetAllProjectsFilteredAsync(title, description, status);
        }

        [HttpGet("{id}")]
        public async Task<ProjectDto> GetProjectById(int id)
        {
            return await _projectService.GetProjectByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var project = await _projectService.CreateProjectAsync(dto);
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] CreateProjectDto dto)
        {
            var updatedProject = await _projectService.UpdateProjectAsync(id, dto);
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            bool deleted = await _projectService.DeleteProjectAsync(id);
            return Ok(deleted);
        }
    }
}
