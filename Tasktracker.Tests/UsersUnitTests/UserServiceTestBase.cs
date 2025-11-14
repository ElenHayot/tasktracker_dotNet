using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Repositories;
using tasktracker.Services;

namespace Tasktracker.Tests.UsersUnitTests
{
    public abstract class UserServiceTestBase
    {
        // Mock instancies
        protected readonly Mock<IUserRepository> MockUserRepo;
        protected readonly Mock<ITaskRepository> MockTaskRepo;
        protected readonly Mock<ILogger<UserService>> MockUserLogger;
        protected readonly UserService UserService;

        /// <summary>
        /// Constructor with mock objects
        /// </summary>
        protected UserServiceTestBase()
        {
            MockUserRepo = new Mock<IUserRepository>();
            MockTaskRepo = new Mock<ITaskRepository>();
            MockUserLogger = new Mock<ILogger<UserService>>();
            UserService = new UserService(MockUserRepo.Object, MockTaskRepo.Object, MockUserLogger.Object);
        }
    }
}
