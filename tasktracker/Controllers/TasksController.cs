using Microsoft.AspNetCore.Mvc;
using tasktracker.DtoModels;
using tasktracker.Enums;
using tasktracker.Services;

namespace tasktracker.Controllers
{
    /// <summary>
    /// Controller for Task part
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

        [HttpGet]
        public async Task<IEnumerable<TaskDto>> GetAllTasksFiltered([FromQuery] string? title, [FromQuery] string? description, [FromQuery] int? projectId, [FromQuery] int? userId, [FromQuery] StatusEnum? status)
        {
            return await _taskService.GetAllTasksFilteredAsync(title, description, projectId, userId, status);
        }

        [HttpGet("{id}")]
        public async Task<TaskDto> GetTaskById(int id)
        {
            return await _taskService.GetTaskByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            return Ok(await  _taskService.CreateTaskAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(int id, [FromBody] CreateTaskDto updates)
        {
            return Ok(await _taskService.UpdateTaskAsync(id, updates));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            return Ok(await _taskService.DeleteTaskAsync(id));
        }
    } 
}
