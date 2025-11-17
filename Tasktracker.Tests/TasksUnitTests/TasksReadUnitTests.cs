using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;

namespace Tasktracker.Tests.TasksUnitTests
{
    /// <summary>
    /// Class to test TaskService Reading part
    /// </summary>
    public class TasksReadUnitTests : TaskServiceTestBase
    {
        /// <summary>
        /// Test getting all tasks without filter applied
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllTasksFilteredAsync_WithNoFilter_ShouldWork()
        {
            List<TaskEntity> tasks = new()
            {
                new TaskEntity() { Id = 1, Title = "Test 1", ProjectId = 1, Status = StatusEnum.New },
                new TaskEntity() { Id = 2, Title = "Test 2", ProjectId = 1, Status = StatusEnum.Pending }
            };

            MockTaskRepo.Setup(repo => repo.GetAllTasksFilteredAsync(It.IsAny<TaskQueryFilter>())).ReturnsAsync(tasks);

            IEnumerable<TaskDto> result = await TaskService.GetAllTasksFilteredAsync(null, null, null, null, null);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            MockTaskRepo.Verify(repo => repo.GetAllTasksFilteredAsync(It.IsAny<TaskQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test getting all tasks with filter applied
        /// </summary>
        /// <param name="title">Filter on 'Name' field</param>
        /// <param name="description">Filter on 'Description' field</param>
        /// <param name="projectId">Filter on 'ProjectId', field</param>
        /// <param name="userId">Filter on 'UserId', field</param>
        /// <param name="status">Filter on 'Status' field</param>
        /// <returns></returns>
        [Theory]
        [InlineData("Test", null, null, null, null)]
        [InlineData(null, "Test", null, null, null)]
        [InlineData(null, null, 1, null, null)]
        [InlineData(null, null, null, 1, null)]
        [InlineData(null, null, null, null, StatusEnum.Pending)]
        public async Task GetAllTaasksFilteredAsync_WithAppliedFilter_ShouldWork(string? title, string? description, int? projectId, int? userId, StatusEnum? status)
        {
            List<TaskEntity> tasks = new()
            {
                new TaskEntity() { Id = 1, Title = "Test 1", Description = "Test task", ProjectId = 1, UserId = 1, Status = StatusEnum.New },
                new TaskEntity() { Id = 2, Title = "Test 2", ProjectId = 1, Status = StatusEnum.Pending }
            };

            MockTaskRepo.Setup(repo => repo.GetAllTasksFilteredAsync(It.IsAny<TaskQueryFilter>())).ReturnsAsync(tasks);

            IEnumerable<TaskDto> result = await TaskService.GetAllTasksFilteredAsync(title, description, projectId, userId, status);
            Assert.NotNull(result);
            Assert.Contains(result, task => task.Title.Contains("Test"));

            MockTaskRepo.Verify(repo => repo.GetAllTasksFilteredAsync(It.IsAny<TaskQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test getting an existing task by its ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTaskByIdAsync_ShouldWork()
        {
            TaskEntity task = new() { Id = 1, Title = "Test 1", Description = "Test task", ProjectId = 1, UserId = 1, Status = StatusEnum.New };

            MockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(task.Id)).ReturnsAsync(task);

            TaskDto result = await TaskService.GetTaskByIdAsync(task.Id);
            Assert.NotNull(result);
            Assert.Equal(task.Title, result.Title);

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(task.Id), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to get a non-existing task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTaskByIdAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(() => TaskService.GetTaskByIdAsync(badId));

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(badId), Times.Once());
        }
    }
}
