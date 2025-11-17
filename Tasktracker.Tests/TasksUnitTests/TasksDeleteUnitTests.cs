using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;
using Tasktracker.Tests.TestData;

namespace Tasktracker.Tests.TasksUnitTests
{
    /// <summary>
    /// Class to test TaskService deleting part
    /// </summary>
    public class TasksDeleteUnitTests : TaskServiceTestBase
    {
        /// <summary>
        /// Test deleting an existing task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTaskAsync_ShouldWork()
        {
            TaskEntity task = TaskTestData.TaskEntityData();
            MockTaskRepo.Setup(repo => repo.GetTaskByIdAsync(task.Id)).ReturnsAsync(task);
            MockTaskRepo.Setup(repo => repo.DeleteTaskAsync(It.IsAny<TaskEntity>())).ReturnsAsync(true);

            bool result = await TaskService.DeleteTaskAsync(task.Id);
            Assert.True(result);

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(task.Id), Times.Once());
            MockTaskRepo.Verify(repo => repo.DeleteTaskAsync(It.IsAny<TaskEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to delete a non-existing task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTaskAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(() => TaskService.DeleteTaskAsync(badId));

            MockTaskRepo.Verify(repo => repo.GetTaskByIdAsync(badId), Times.Once());
            MockTaskRepo.Verify(repo => repo.DeleteTaskAsync(It.IsAny<TaskEntity>()), Times.Never());
        }
    }
}
