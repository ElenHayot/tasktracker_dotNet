using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Mappers;
using tasktracker.Repositories;
using tasktracker.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SQLitePCL;
using Microsoft.AspNetCore.Http.HttpResults;

namespace tasktracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            UserEntity userEntity = UserMapper.ToCreateEntity(userDto);
            UserEntity createdUser = await _userRepository.CreateUserAsync(userEntity);
            UserDto user = UserMapper.ToDto(createdUser);

            return user;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<UserDto>> GetAllUsersFilteredAsync(string? name, string? firstname, RolesEnum? role)
        {
            UserQueryFilter filter = new UserQueryFilter
            {
                Name = name,
                Firstname = firstname,
                Role = role
            };

            IEnumerable<UserEntity> userList = await _userRepository.GetAllUsersFilteredAsync(filter);
            List<UserDto> users = userList.Select(UserMapper.ToDto).ToList();

            return users;
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            UserEntity? user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException($"User with email '{email}' not found.");
            }
            
            return UserMapper.ToDto(user);
        }

        /// <inheritdoc/>
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }

            return UserMapper.ToDto(user);
        }

        /// <inheritdoc/>
        public async Task<UserDto> UpdateUserAsync(int id, CreateUserDto userDto)
        {
            // On récupère le user entity existant en base
            UserEntity? existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }

            // On construit le nouveau user entity
            UserEntity updatedUser = new()
            {
                Id = id,
                Name = userDto.Name,
                Firstname = userDto.Firstname,
                Email = userDto.Email,
                Role = userDto.Role,
                PasswordHash = existingUser.PasswordHash
            };

            // Update du user entity existant avec le nouveau user entity
            updatedUser = await _userRepository.UpdateUserAsync(existingUser, updatedUser);
            // Construction d'un DTO
            UserDto updatedUserDto = UserMapper.ToDto(updatedUser);

            return updatedUserDto;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteUserAsync(int id)
        {
            UserEntity? user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }
            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
