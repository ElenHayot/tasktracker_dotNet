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

namespace Tasktracker.Tests.ProjectsUnitTests
{
    /// <summary>
    /// Class to test ProjectService Updating part
    /// </summary>
    public class ProjectsUpdateUnitTests : ProjectServiceTestBase
    {
        /// <summary>
        /// Test updating a project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateProjectAsync_ShouldWork()
        {
            ProjectEntity existingProject = ProjectTestData.ProjectEntityData();

            UpdateProjectDto updatedProjectDto = new()
            {
                Description = "Add description"
            };

            ProjectEntity expectedProject = new()
            {
                Id = existingProject.Id,
                Title = existingProject.Title,
                Description = updatedProjectDto.Description ?? existingProject.Description,
                Status = updatedProjectDto.Status ?? existingProject.Status
            };

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(existingProject.Id)).ReturnsAsync(existingProject);
            MockProjectRepo.Setup(repo => repo.UpdateProjectAsync(It.IsAny<ProjectEntity>(), It.IsAny<ProjectEntity>())).ReturnsAsync(expectedProject);

            ProjectDto result = await ProjectService.UpdateProjectAsync(existingProject.Id, updatedProjectDto);

            Assert.NotNull(result);
            Assert.NotNull(result.Description);
            Assert.Equal(updatedProjectDto.Description, result.Description);

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(existingProject.Id), Times.Once());
            MockProjectRepo.Verify(repo => repo.UpdateProjectAsync(It.IsAny<ProjectEntity>(), It.IsAny<ProjectEntity>()), Times.Once());
        }

        /// <summary>
        /// Test updating a project with nothing to update (with an empty UpdateProjectDto)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateProjectAsync_WithNoUpdate_ShouldWork()
        {
            ProjectEntity existingProject = ProjectTestData.ProjectEntityData();

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(existingProject.Id)).ReturnsAsync(existingProject);
            MockProjectRepo.Setup(repo => repo.UpdateProjectAsync(It.IsAny<ProjectEntity>(), It.IsAny<ProjectEntity>())).ReturnsAsync(existingProject);

            ProjectDto result = await ProjectService.UpdateProjectAsync(existingProject.Id, new UpdateProjectDto());
            Assert.NotNull(result);
            Assert.Equal(existingProject.Status, result.Status);

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(existingProject.Id), Times.Once());
            MockProjectRepo.Verify(repo => repo.UpdateProjectAsync(It.IsAny<ProjectEntity>(), It.IsAny<ProjectEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exeption when trying to updte a non-existing project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateProjectAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(
                () => ProjectService.UpdateProjectAsync(badId, new UpdateProjectDto()));

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(badId), Times.Once());
            MockProjectRepo.Verify(repo => repo.UpdateProjectAsync(It.IsAny<ProjectEntity>(), It.IsAny<ProjectEntity>()), Times.Never());
        }
    }
}
