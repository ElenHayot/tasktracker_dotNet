using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using Tasktracker.Tests.TestData;

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
            CreateProjectDto projectDto = ProjectTestData.CreateProjectData();

            ProjectEntity expectedProject = ProjectTestData.ProjectEntityData();

            MockProjectRepo
                .Setup(repo => repo.CreateProjectAsync(It.IsAny<ProjectEntity>()))
                .ReturnsAsync(expectedProject);

            ProjectDto result = await ProjectService.CreateProjectAsync(projectDto);
            Assert.NotNull(result);
            Assert.Equal(expectedProject.Id, result.Id);

            MockProjectRepo.Verify(repo => repo.CreateProjectAsync(It.IsAny<ProjectEntity>()), Times.Once());
        }
    }
}
