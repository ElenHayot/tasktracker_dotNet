using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Repositories;
using tasktracker.Services;

namespace Tasktracker.Tests.ProjectsUnitTests
{
    /// <summary>
    /// Mock initialisation class
    /// </summary>
    public abstract class ProjectServiceTestBase
    {
        #region Mock instancies
        protected readonly Mock<IProjectRepository> MockProjectRepo;
        protected readonly Mock<ITaskRepository> MockTaskRepo;
        protected readonly Mock<ILogger<ProjectService>> MockProjectLogger;
        protected readonly ProjectService ProjectService;
        #endregion

        /// <summary>
        /// Constructor with mock objects
        /// </summary>
        public ProjectServiceTestBase()
        {
            MockProjectRepo = new Mock<IProjectRepository>();
            MockTaskRepo = new Mock<ITaskRepository>();
            MockProjectLogger = new Mock<ILogger<ProjectService>>();
            ProjectService = new ProjectService(MockProjectRepo.Object, MockTaskRepo.Object, MockProjectLogger.Object);
        }
    }
}
