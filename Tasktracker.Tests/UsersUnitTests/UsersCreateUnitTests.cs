using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Common;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Exceptions;
using tasktracker.Repositories;
using tasktracker.Services;

namespace Tasktracker.Tests.UsersUnitTests
{
    /// <summary>
    /// Class to test UserService Creating part 
    /// </summary>
    public class UsersCreateUnitTests : UserServiceTestBase
    {
        /// <summary>
        /// Test creating a user with good CreateUserDto
        /// Should work
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUserAsync_ShouldWork()
        {

            // Input DTO
            CreateUserDto inputDto = new()
            {
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Role = RolesEnum.Admin,
                Password = "StrongPassword...Isn'tIt?"
            };

            // Expected entity to be returned
            UserEntity expectedEntity = new()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Role = RolesEnum.Admin,
                PasswordHash = PasswordHelper.HashPassword("StrongPassword...Isn'tIt?")
            };

            // Configurer le comportement du mock pour ce test
            MockUserRepo
                .Setup(repo => repo.CreateUserAsync(It.IsAny<UserEntity>()))    // "Quand on appelle CreateUserAsync avec n'importe quelle UserEntity"
                .ReturnsAsync(expectedEntity);  // "Alors retourne cette entité"

            // Call the method
            UserDto result = await UserService.CreateUserAsync(inputDto);

            // Expected result
            Assert.NotNull(result);
            Assert.Equal("Doe", result.Name);
            Assert.Equal(1, result.Id);

            MockUserRepo.Verify(repo => repo.CreateUserAsync(It.IsAny<UserEntity>()), Times.Once());
        }

        /// <summary>
        /// Test EmailAlreadyExistsException when creating a user with an existing email
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateUserAsync_WrongEmailType_ShouldNotWork()
        {
            CreateUserDto inputDto = new()
            {
                Name = "Doe",
                Firstname = "John",
                Email = "johndoe@example.com",
                Role = RolesEnum.Admin,
                Password = "StrongPassword...Isn'tIt?"
            };

            // Expected entity to be returned
            UserEntity expectedEntity = new()
            {
                Id = 1,
                Name = "Doe",
                Firstname = "John",
                Email = "johhdoe@example.com",
                Role = RolesEnum.Admin,
                PasswordHash = PasswordHelper.HashPassword("StrongPassword...Isn'tIt?")
            };

            // Simule que le user inputDto existe déjà
            MockUserRepo
                .Setup(repo => repo.GetUserByEmailAsync(inputDto.Email))
                .ReturnsAsync(expectedEntity);

            await Assert.ThrowsAsync<EmailAlreadyExistsException>(
                () => UserService.CreateUserAsync(inputDto));

            // Vérifier que CreateUserAsync n'a jamais été appelé
            MockUserRepo.Verify(repo => repo.CreateUserAsync(It.IsAny<UserEntity>()), Times.Never());

        }
    }
}
