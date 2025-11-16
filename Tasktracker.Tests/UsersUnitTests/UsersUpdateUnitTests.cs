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

namespace Tasktracker.Tests.UsersUnitTests
{
    /// <summary>
    /// Class to test UserService Updating part
    /// </summary>
    public class UsersUpdateUnitTests : UserServiceTestBase
    {
        /// <summary>
        /// Test updating an existing user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_ShouldWork()
        {
            var user = new UserEntity()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Phone = "0123456789",
                Role = RolesEnum.Admin,
                PasswordHash = ""
            };

            var updatedUserDto = new UpdateUserDto()
            {
                Email = "changedEmail@example.com",
                Phone = "0199999999",
                Role = RolesEnum.User
            };

            var expectedUser = new UserEntity()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "changedEmail@example.com",
                Phone = "0199999999",
                Role = RolesEnum.User,
                PasswordHash = ""
            };

            MockUserRepo.Setup(repo => repo.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
            MockUserRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>())).ReturnsAsync(expectedUser);

            var result = await UserService.UpdateUserAsync(user.Id, updatedUserDto);

            Assert.NotNull(result);
            Assert.Equal("changedEmail@example.com", result.Email);
            Assert.Equal(RolesEnum.User, result.Role);

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(user.Id), Times.Once());
            MockUserRepo.Verify(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>()), Times.Once());
        }

        /// <summary>
        /// Test updating a user with nothing to update
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithNoUpdate_ShouldWork()
        {
            var user = new UserEntity()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Phone = "0123456789",
                Role = RolesEnum.Admin,
                PasswordHash = ""
            };

            // Empty UpdateUserDto to update nothing
            var updatedUserDto = new UpdateUserDto();

            MockUserRepo.Setup(repo => repo.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
            MockUserRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>())).ReturnsAsync(user);

            var result = await UserService.UpdateUserAsync(user.Id, updatedUserDto);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Role, result.Role);
            Assert.Equal(user.Phone, result.Phone);

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(user.Id), Times.Once());
            MockUserRepo.Verify(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>()), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to update a non-existing user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithWrongUserId_ShouldNotWork()
        {
            int badId = 1;
            await Assert.ThrowsAsync<NotFoundException>(
                () => UserService.UpdateUserAsync(badId, new UpdateUserDto()));

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(badId), Times.Once());
            MockUserRepo.Verify(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>()), Times.Never());
        }
    }
}
