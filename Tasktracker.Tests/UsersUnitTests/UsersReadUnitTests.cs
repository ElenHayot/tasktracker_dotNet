using Moq;
using NuGet.Frameworks;
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
    /// Class to test UserService Reading part
    /// </summary>
    public class UsersReadUnitTests : UserServiceTestBase
    {
        /// <summary>
        /// Test getting all users without filter applied
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllUsersFilteredAsync_WithoutFilter_ShouldWork()
        {
            // Entities
            List<UserEntity> allUsers = new()
            {
                UserTestData.UserEntityData(1),
                UserTestData.UserEntityData(2),
                UserTestData.UserEntityData(3)
            };

            // Return allUsers list 
            MockUserRepo
                .Setup(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()))
                .ReturnsAsync(allUsers);

            IEnumerable<UserDto> result = await UserService.GetAllUsersFilteredAsync(null, null, null, null);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, user => user.Email.Contains("1"));
            Assert.Contains(result, user => user.Email.Contains("2"));
            Assert.Contains(result, user => user.Email.Contains("3"));

            MockUserRepo.Verify(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test getting users with filter applied
        /// </summary>
        /// <param name="name">Filter on 'Name' field</param>
        /// <param name="firstname">Filter on 'Firstname' field</param>
        /// <param name="phone">Filter on 'Phone' field</param>
        /// <param name="role">Filter on 'Role' field</param>
        /// <returns></returns>
        [Theory]
        [InlineData("One", null, null, null)]
        [InlineData(null, "User", null, null)]
        [InlineData(null, null, "0123456789", null)]
        [InlineData(null, null, null, RolesEnum.Moderator)]
        public async Task GetAllUsersFilteredAsync_WithRoleFilter_ShouldWork(string? name, string? firstname, string? phone, RolesEnum? role)
        {
            // Entities
            List<UserEntity> allUsers = new()
            {
                new UserEntity(){ Id = 1, Name = "One", Firstname = "User", Email = "oneuser@example.com", Phone = "0123456789", Role = RolesEnum.Admin, PasswordHash = ""},
                new UserEntity(){ Id = 2, Name = "Two", Firstname = "User", Email = "twouser@example.com", Role = RolesEnum.User, PasswordHash = ""},
                new UserEntity(){ Id = 3, Name = "Three", Firstname = "User", Email = "threeuser@example.com", Role = RolesEnum.Moderator, PasswordHash = ""}
            };

            // Return allUsers list 
            MockUserRepo
                .Setup(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()))
                .ReturnsAsync(allUsers);

            IEnumerable<UserDto> result = await UserService.GetAllUsersFilteredAsync(name, firstname, phone, role);
            Assert.NotNull(result);

            MockUserRepo.Verify(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()), Times.Once());
        }

        /// <summary>
        /// Test getting an existing user by its ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByIdAsync_ShouldWork()
        {
            UserEntity user = UserTestData.UserEntityData();

            MockUserRepo
                .Setup(repo => repo.GetUserByIdAsync(user.Id))
                .ReturnsAsync(user);

            UserDto result = await UserService.GetUserByIdAsync(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Doe", result.Name);

            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(user.Id), Times.Once());
        }

        /// <summary>
        /// Test throwing an exception when trying to get a non-existing user by its ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByIdAsync_WithWrongId_ShouldNotWork()
        {
            int badId = 1;
            // Expect a NotFoundException when asking for a non-existing user
            await Assert.ThrowsAsync<NotFoundException>(
                () => UserService.GetUserByIdAsync(badId));

            // Verify it called the repository function !!
            MockUserRepo.Verify(repo => repo.GetUserByIdAsync(badId), Times.Once());

        }

        /// <summary>
        /// Test getting an existing user by its email
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByEmailAsync_ShouldWork()
        {
            UserEntity user = UserTestData.UserEntityData();

            MockUserRepo
                .Setup(repo => repo.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            UserDto result = await UserService.GetUserByEmailAsync(user.Email);

            Assert.NotNull(result);
            Assert.Equal("Doe", result.Name);

            MockUserRepo.Verify(repo => repo.GetUserByEmailAsync(user.Email), Times.Once());
        }

        /// <summary>
        /// Test throwing an when trying to get a non-existing user by its email
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUserByEmailAsync_WithWrongEmail_ShouldNotWork()
        {
            string badEmail = "toto@example.com";
            await Assert.ThrowsAsync<NotFoundException>(
                () => UserService.GetUserByEmailAsync(badEmail));

            MockUserRepo.Verify(repo => repo.GetUserByEmailAsync(badEmail), Times.Once());
        }
    }
}
