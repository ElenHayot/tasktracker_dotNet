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
    /// <summary>
    /// User service - manage Users calls
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Local user repository instance
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// UserService constructor
        /// </summary>
        /// <param name="userRepository">User repository instance</param>
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
            // Get existing user in db
            UserEntity? existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }

            // Build new user
            UserEntity updatedUser = new()
            {
                Id = id,
                Name = userDto.Name,
                Firstname = userDto.Firstname,
                Email = userDto.Email,
                Role = userDto.Role,
                PasswordHash = existingUser.PasswordHash
            };

            // Update existingUser with updatedUser data
            updatedUser = await _userRepository.UpdateUserAsync(existingUser, updatedUser);
            // DTO to send back
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
