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
    /// Class to test ProjectService Reading part
    /// </summary>
    public class ProjectsReadUnitTests : ProjectServiceTestBase
    {
        /// <summary>
        /// Test getting all projects without filter applied
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllProjectsFilteredAsync_WithEmptyFilter_ShouldWork()
        {
            // Building fake datas
            List<ProjectEntity> projects = new()
            {
                ProjectTestData.ProjectEntityData(1),
                ProjectTestData.ProjectEntityData(2),
                ProjectTestData.ProjectEntityData(3)
            };

            // Send back the projects list when asking
            MockProjectRepo
                .Setup(repo => repo.GetAllProjectsFilteredAsync(It.IsAny<ProjectQueryFilter>()))
                .ReturnsAsync(projects);

            IEnumerable<ProjectDto> result = await ProjectService.GetAllProjectsFilteredAsync(null, null, null);
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

            MockProjectRepo.Verify(repo => repo.GetAllProjectsFilteredAsync(It.IsAny<ProjectQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test get all projects with filter applied
        /// </summary>
        /// <param name="title">Filter on 'Title' field</param>
        /// <param name="description">Filter on 'Description' field</param>
        /// <param name="status">Filter on 'Status' field</param>
        /// <returns></returns>
        [Theory]
        [InlineData("Title", null, null)]
        [InlineData(null, "Test", null)]
        [InlineData(null, null, StatusEnum.New)]
        public async Task GetAllProjectsFilteredAsync_WithFilter_ShouldWork(string? title, string? description, StatusEnum? status)
        {
            // Building fake datas
            List<ProjectEntity> projects = new()
            {
                new ProjectEntity() { Id = 1, Title = "Test 1", Status = StatusEnum.New },
                new ProjectEntity() { Id = 2, Title = "Test 2", Description = "Test my project", Status = StatusEnum.Pending },
                new ProjectEntity() { Id = 3, Title = "Test 3", Status = StatusEnum.Completed }
            };

            // Send back the projects list when asking
            MockProjectRepo
                .Setup(repo => repo.GetAllProjectsFilteredAsync(It.IsAny<ProjectQueryFilter>()))
                .ReturnsAsync(projects);

            IEnumerable<ProjectDto> result = await ProjectService.GetAllProjectsFilteredAsync(title, description, status);
            Assert.NotNull(result);

            MockProjectRepo.Verify(repo => repo.GetAllProjectsFilteredAsync(It.IsAny<ProjectQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test getting an existing project by its ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProjectByIdAsync_ShouldWork()
        {
            ProjectEntity project = ProjectTestData.ProjectEntityData();

            MockProjectRepo.Setup(repo => repo.GetProjectByIdAsync(project.Id)).ReturnsAsync(project);

            ProjectDto result = await ProjectService.GetProjectByIdAsync(project.Id);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(project.Id), Times.Once());
        }

        /// <summary>
        /// Test getting a non-existing project by its ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetProjectByIdAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(
                () => ProjectService.GetProjectByIdAsync(badId));

            MockProjectRepo.Verify(repo => repo.GetProjectByIdAsync(badId), Times.Once());
        }
    }
}
