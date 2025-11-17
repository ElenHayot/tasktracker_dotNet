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
using Tasktracker.Tests.TestData;

namespace Tasktracker.Tests.TasksUnitTests
{
    /// <summary>
    /// Class to test TaskService Updating part
    /// </summary>
    public class TasksUpdateUnitTest : TaskServiceTestBase
    {
        /// <summary>
        /// Test updating an existing task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTaskAsync_ShouldWork()
        {
            TaskEntity existingTask = TaskTestData.TaskEntityData();
            UpdateTaskDto updatedTaskDto = new() 
            { 
                Description = "New description", 
                Status = StatusEnum.Pending 
            };
            TaskEntity expectedTask = new() 
            { 
                Id = existingTask.Id, 
                Title = existingTask.Title, 
                ProjectId = existingTask.ProjectId, 
                Description = updatedTaskDto.Description ?? existingTask.Description, 
                Status = updatedTaskDto.Status ?? existingTask.Status 
            };

            MockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(existingTask.Id)).ReturnsAsync(existingTask);
            MockTaskRepo.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>(), It.IsAny<TaskEntity>())).ReturnsAsync(expectedTask);

            TaskDto result = await TaskService.UpdateTaskAsync(existingTask.Id, updatedTaskDto);
            Assert.NotNull(result);
            Assert.Equal(expectedTask.Description, result.Description);
            Assert.Equal(expectedTask.Status, result.Status);

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(existingTask.Id), Times.Once);
            MockTaskRepo.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>(), It.IsAny<TaskEntity>()), Times.Once());
        }

        /// <summary>
        /// Test updating a task with nothing to update
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTaskAsync_WithNoUpdate_ShouldWork()
        {
            TaskEntity existingTask = TaskTestData.TaskEntityData();
            MockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(existingTask.Id)).ReturnsAsync(existingTask);
            MockTaskRepo.Setup(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>(), It.IsAny<TaskEntity>())).ReturnsAsync(existingTask);

            TaskDto result = await TaskService.UpdateTaskAsync(existingTask.Id, new UpdateTaskDto());
            Assert.NotNull(result);
            Assert.Equal(existingTask.Id, result.Id);

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(existingTask.Id), Times.Once);
            MockTaskRepo.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>(), It.IsAny<TaskEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to update a non-existing task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateTaskAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(() => TaskService.UpdateTaskAsync(badId, new UpdateTaskDto()));

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(badId), Times.Once);
            MockTaskRepo.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>(), It.IsAny<TaskEntity>()), Times.Never());
        }
    }
}
