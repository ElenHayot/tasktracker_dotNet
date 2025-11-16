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
            var allUsers = new List<UserEntity>()
            {
                new UserEntity(){ Id = 1, Name = "One", Firstname = "User", Email = "oneuser@example.com", Role = RolesEnum.Admin, PasswordHash = ""},
                new UserEntity(){ Id = 2, Name = "Two", Firstname = "User", Email = "twouser@example.com", Role = RolesEnum.User, PasswordHash = ""},
                new UserEntity(){ Id = 3, Name = "Three", Firstname = "User", Email = "threeuser@example.com", Role = RolesEnum.Moderator, PasswordHash = ""}
            };

            // Return allUsers list 
            MockUserRepo
                .Setup(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()))
                .ReturnsAsync(allUsers);

            var result = await UserService.GetAllUsersFilteredAsync(null, null, null, null);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, user => user.Name == "One");
            Assert.Contains(result, user => user.Name == "Two");
            Assert.Contains(result, user => user.Name == "Three");

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
            var allUsers = new List<UserEntity>()
            {
                new UserEntity(){ Id = 1, Name = "One", Firstname = "User", Email = "oneuser@example.com", Phone = "0123456789", Role = RolesEnum.Admin, PasswordHash = ""},
                new UserEntity(){ Id = 2, Name = "Two", Firstname = "User", Email = "twouser@example.com", Role = RolesEnum.User, PasswordHash = ""},
                new UserEntity(){ Id = 3, Name = "Three", Firstname = "User", Email = "threeuser@example.com", Role = RolesEnum.Moderator, PasswordHash = ""}
            };

            // Return allUsers list 
            MockUserRepo
                .Setup(repo => repo.GetAllUsersFilteredAsync(It.IsAny<UserQueryFilter>()))
                .ReturnsAsync(allUsers);

            var result = await UserService.GetAllUsersFilteredAsync(name, firstname, phone, role);
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

            MockUserRepo
                .Setup(repo => repo.GetUserByIdAsync(user.Id))
                .ReturnsAsync(user);

            var result = await UserService.GetUserByIdAsync(user.Id);

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

            MockUserRepo
                .Setup(repo => repo.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var result = await UserService.GetUserByEmailAsync(user.Email);

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
