using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;

namespace Tasktracker.Tests.UsersUnitTests
{
    /// <summary>
    /// Class to test UserService Deleting part
    /// </summary>
    public class UsersDeleteUnitTests : UserServiceTestBase
    {
        /// <summary>
        /// Test deleting an existing user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUserAsync_ShoudlWork()
        {
            UserEntity user = new()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Phone = "0123456789",
                Role = RolesEnum.Admin,
                PasswordHash = ""
            };

            MockUserRepo.Setup(repo => repo.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
            MockUserRepo.Setup(repo => repo.DeleteUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(true);

            bool result = await UserService.DeleteUserAsync(user.Id);
            Assert.True(result);

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(It.IsAny<int>()), Times.Once());
            MockUserRepo.Verify(repo => repo.DeleteUserAsync(It.IsAny<UserEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to delete a non-existing user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUserAsync_WithWrongUserId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(
                () => UserService.DeleteUserAsync(badId));

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(badId), Times.Once());
            MockUserRepo.Verify(repo => repo.DeleteUserAsync(It.IsAny<UserEntity>()), Times.Never());
        }
    }
}
