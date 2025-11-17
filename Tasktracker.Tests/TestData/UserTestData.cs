using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasktracker.Common;
using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;

namespace Tasktracker.Tests.TestData
{
    public class UserTestData
    {
        public static CreateUserDto CreateUserData(int id = 1)
        {
            return new CreateUserDto()
            {
                Name = "Doe",
                Firstname = "John",
                Email = $"johndoe{id}@example.com",
                Role = RolesEnum.Admin,
                Password = "StrongPassword...Isn'tIt?"
            };
        }

        public static UpdateUserDto UpdateUserData()
        {
            return new UpdateUserDto()
            {
                Email = "changedEmail@example.com",
                Phone = "0199999999",
                Role = RolesEnum.User
            };
        }

        public static UserEntity UserEntityData (int id = 1)
        {
            return new UserEntity()
            {
                Id = id,
                Name = "Doe",
                Firstname = "John",
                Email = $"johndoe{id}@example.com",
                Role = RolesEnum.Admin,
                PasswordHash = PasswordHelper.HashPassword("StrongPassword...Isn'tIt?")
            };
        }
    }
}
