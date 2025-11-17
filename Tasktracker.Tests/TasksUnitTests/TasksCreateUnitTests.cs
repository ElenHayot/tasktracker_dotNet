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
    /// Class to test TaskService Creating part
    /// </summary>
    public class TasksCreateUnitTests : TaskServiceTestBase
    {
        /// <summary>
        /// Test creating a task with an existing project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTaskAsync_ShouldWork()
        {
            ProjectEntity project = new()
            {
                Id = 1,
                Title = "Test",
                Status = StatusEnum.InProgress
            };

            CreateTaskDto taskDto = new()
            {
                Title = "Test",
                ProjectId = 1,
                Status = StatusEnum.New
            };

            TaskEntity expectedEntity = new()
            {
                Id = 1,
                Title = "Test",
                ProjectId = 1,
                Status = StatusEnum.New
            };

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);
            MockTaskRepo.Setup(repo => repo.CreateTaskAsync(It.IsAny<TaskEntity>())).ReturnsAsync(expectedEntity);

            TaskDto result = await TaskService.CreateTaskAsync(taskDto);
            Assert.NotNull(result);
            Assert.Equal(expectedEntity.Id, result.Id);
            Assert.Equal(expectedEntity.Title, result.Title);

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(project.Id), Times.Once());
            MockTaskRepo.Verify(repo => repo.CreateTaskAsync(It.IsAny<TaskEntity>()), Times.Once());
        }

        /// <summary>
        /// Test creating a task with a non-existing associated project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTaskAsync_WithWrongProjectId_ShouldNotWork()
        {
            int badProjectId = 1;
            CreateTaskDto taskDto = new()
            {
                Title = "Test",
                ProjectId = badProjectId,
                Status = StatusEnum.New
            };
            await Assert.ThrowsAsync<AssociatedProjectNotFound>(() =>  TaskService.CreateTaskAsync(taskDto));

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(badProjectId), Times.Once());
            MockTaskRepo.Verify(repo => repo.CreateTaskAsync(It.IsAny<TaskEntity>()), Times.Never());
        }

        /// <summary>
        /// Test creating a task with a non-existing associated user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTaskAsync_WithWrongUserId_ShouldNotWork()
        {
            int badUserId = 1;
            ProjectEntity project = new()
            {
                Id = 1,
                Title = "Test",
                Status = StatusEnum.InProgress
            };

            CreateTaskDto taskDto = new()
            {
                Title = "Test",
                ProjectId = 1,
                Status = StatusEnum.New,
                UserId = badUserId
            };

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);

            await Assert.ThrowsAsync<AssociatedUserNotFound>(() =>  TaskService.CreateTaskAsync(taskDto));
            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(project.Id), Times.Once());
            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(badUserId), Times.Once());
            MockTaskRepo.Verify(repo => repo.CreateTaskAsync(It.IsAny<TaskEntity>()), Times.Never());
        }
    }
}
