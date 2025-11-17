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
            UserEntity existingUser = UserTestData.UserEntityData();

            UpdateUserDto updatedUserDto = new()
            {
                Email = "changedEmail@example.com",
                Phone = "0199999999",
                Role = RolesEnum.User
            };

            UserEntity expectedUser = new()
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Firstname = existingUser.Firstname,
                Email = updatedUserDto.Email ?? existingUser.Email,
                Phone = updatedUserDto.Phone ?? existingUser.Phone,
                Role = updatedUserDto.Role ?? existingUser.Role,
                PasswordHash = ""
            };

            MockUserRepo.Setup(repo => repo.GetUserByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            MockUserRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>())).ReturnsAsync(expectedUser);

            UserDto result = await UserService.UpdateUserAsync(existingUser.Id, updatedUserDto);

            Assert.NotNull(result);
            Assert.Equal("changedEmail@example.com", result.Email);
            Assert.Equal(RolesEnum.User, result.Role);

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(existingUser.Id), Times.Once());
            MockUserRepo.Verify(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>()), Times.Once());
        }

        /// <summary>
        /// Test updating a user with nothing to update
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateUserAsync_WithNoUpdate_ShouldWork()
        {
            UserEntity user = UserTestData.UserEntityData();

            MockUserRepo.Setup(repo => repo.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
            MockUserRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserEntity>(), It.IsAny<UserEntity>())).ReturnsAsync(user);

            UserDto result = await UserService.UpdateUserAsync(user.Id, new UpdateUserDto());

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
