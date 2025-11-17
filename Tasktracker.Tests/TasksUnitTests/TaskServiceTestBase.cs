using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Repositories;
using tasktracker.Services;

namespace Tasktracker.Tests.TasksUnitTests
{
    /// <summary>
    /// Mock initialisation class
    /// </summary>
    public abstract class TaskServiceTestBase
    {
        #region Mock instancies
        protected readonly Mock<ITaskRepository> MockTaskRepo;
        protected readonly Mock<IProjectRepository> MockProjectRepo;
        protected readonly Mock<IUserRepository> MockUserRepo;
        protected readonly Mock<ILogger<TaskService>> MockTaskLogger;
        protected readonly TaskService TaskService;
        #endregion

        /// <summary>
        /// Constructor with mock objects
        /// </summary>
        protected TaskServiceTestBase()
        {
            MockTaskRepo = new Mock<ITaskRepository>();
            MockProjectRepo = new Mock<IProjectRepository>();
            MockUserRepo = new Mock<IUserRepository>();
            MockTaskLogger = new Mock<ILogger<TaskService>>();
            TaskService = new TaskService(MockTaskRepo.Object, MockProjectRepo.Object, MockUserRepo.Object, MockTaskLogger.Object);
        }
    }
}
