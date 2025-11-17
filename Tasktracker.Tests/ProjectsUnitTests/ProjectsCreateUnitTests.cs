using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;

namespace Tasktracker.Tests.ProjectsUnitTests
{
    /// <summary>
    /// Class to test ProjectService Creating part
    /// </summary>
    public class ProjectsCreateUnitTests : ProjectServiceTestBase
    {
        /// <summary>
        /// Test creating a new project
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateProjectAsync_ShouldWork()
        {
            CreateProjectDto projectDto = new()
            {
                Title = "Test",
                Description = "Test",
                Status = StatusEnum.New
            };

            ProjectEntity expectedProject = new()
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                Status = StatusEnum.New
            };

            MockProjectRepo
                .Setup(repo => repo.CreateProjectAsync(It.IsAny<ProjectEntity>()))
                .ReturnsAsync(expectedProject);

            ProjectDto result = await ProjectService.CreateProjectAsync(projectDto);
            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);

            MockProjectRepo.Verify(repo => repo.CreateProjectAsync(It.IsAny<ProjectEntity>()), Times.Once());
        }
    }
}
