using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Task controller - manage Tasks part
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]  // api/v1/tasks
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// Local task service instance
        /// </summary>
        private readonly ITaskService _taskService;

        /// <summary>
        /// TaskController constructor
        /// </summary>
        /// <param name="taskService">Task service instance</param>
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Call Service.GetAllTasksFilteredAsync
        /// </summary>
        /// <param name="title">FromQuery parameter 'title' - string</param>
        /// <param name="description">FromQuery parameter 'description' - string</param>
        /// <param name="projectId">FromQuery parameter projectId - integer</param>
        /// <param name="userId">FromQuery parameter userId - integer</param>
        /// <param name="status">FromQuery parameter status - Enum value</param>
        /// <returns>List of tasks</returns>
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<TaskDto>> GetAllTasksFiltered([FromQuery] string? title, [FromQuery] string? description, [FromQuery] int? projectId, [FromQuery] int? userId, [FromQuery] StatusEnum? status)
        {
            return await _taskService.GetAllTasksFilteredAsync(title, description, projectId, userId, status);
        }

        /// <summary>
        /// Call Service.GetTaskByIdAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>One task</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            try
            {
                return Ok(await _taskService.GetTaskByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Call Service.CreateTaskAsync
        /// </summary>
        /// <param name="dto">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            try
            {
                return Ok(await _taskService.CreateTaskAsync(dto));
            }
            catch (AssociatedProjectNotFound ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (AssociatedUserNotFound ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (TitleAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Call Service.UpdateTaskAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <param name="updates">FromBody object</param>
        /// <returns>Ok/Nok result</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updates)
        {
            try
            {
                return Ok(await _taskService.UpdateTaskAsync(id, updates));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Call Service.DeleteTaskAsync
        /// </summary>
        /// <param name="id">URL parameter - integer</param>
        /// <returns>Ok/Nok result</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    } 
}
