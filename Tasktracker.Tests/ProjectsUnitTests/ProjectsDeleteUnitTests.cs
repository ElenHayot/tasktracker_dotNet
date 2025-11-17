using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Repositories;
using Tasktracker.Tests.TestData;

namespace Tasktracker.Tests.ProjectsUnitTests
{
    /// <summary>
    /// Class to test ProjectService Deleting part
    /// </summary>
    public class ProjectsDeleteUnitTests : ProjectServiceTestBase
    {
        /// <summary>
        /// Test deleting an existing project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteProjectAsync_ShouldWork()
        {
            ProjectEntity project = ProjectTestData.ProjectEntityData();

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);
            MockProjectRepo.Setup(repo => repo.DeleteProjectAsync(It.IsAny<ProjectEntity>())).ReturnsAsync(true);

            bool result = await ProjectService.DeleteProjectAsync(project.Id);

            Assert.True(result);

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(project.Id), Times.Once());
            MockProjectRepo.Verify(repo => repo.DeleteProjectAsync(It.IsAny<ProjectEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to delete a non-existing project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteProjectAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(() => ProjectService.DeleteProjectAsync(badId));

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(badId), Times.Once());
            MockProjectRepo.Verify(repo => repo.DeleteProjectAsync(It.IsAny<ProjectEntity>()), Times.Never());
        }
    }
}
