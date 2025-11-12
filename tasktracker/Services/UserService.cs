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
using System.ComponentModel.DataAnnotations;

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
        /// Local task repository instance
        /// </summary>
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Local logger instance for UserService
        /// </summary>
        private readonly ILogger<UserService> _logger;

        /// <summary>
        /// UserService constructor
        /// </summary>
        /// <param name="userRepository">User repository instance</param>
        /// <param name="taskRepository">Task repository instance</param>
        /// <param name="logger">User service logger instance</param>
        public UserService(IUserRepository userRepository, ITaskRepository taskRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _logger = logger;
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
        public async Task<IEnumerable<UserDto>> GetAllUsersFilteredAsync(string? name, string? firstname, string? phone, RolesEnum? role)
        {
            UserQueryFilter filter = new UserQueryFilter
            {
                Name = name,
                Firstname = firstname,
                Phone = phone,
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
        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updatedUser)
        {
            // Get existing user in db
            UserEntity? existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }

            // Build new user
            UserEntity updatedEntity = UserMapper.ToUpdateUser(existingUser, updatedUser);

            // Verify TaskIds list
            if (existingUser.TaskIds == null && updatedEntity.TaskIds != null) { 
                // If new taskIds then associate
                foreach (int i in updatedEntity.TaskIds)
                {
                    TaskEntity? taskToUpdate = await _taskRepository.GetTaskByIdAsync(i);
                    if (taskToUpdate != null)
                        await AssociateUserToTask(existingUser.Id, taskToUpdate);
                    else
                    {
                        _logger.LogWarning($"Task with id '{i}' does not exist - has been removed from user tasks list.");
                        updatedEntity.TaskIds.Remove(i);
                    }
                }
            }
            else if (existingUser.TaskIds != null && updatedEntity.TaskIds == null)
            {
                // If old task IDS then dissociate
                foreach (int i in existingUser.TaskIds)
                {
                    TaskEntity? taskToUpdate = await _taskRepository.GetTaskByIdAsync(i);
                    if (taskToUpdate != null)
                        await DissociateUserFromTask(existingUser.Id, taskToUpdate);
                    else
                        _logger.LogInformation($"Task with id '{i}' not found - ignored");
                }
            }
            else if (existingUser.TaskIds != null && updatedEntity.TaskIds != null && !existingUser.TaskIds.ToHashSet().SetEquals(updatedEntity.TaskIds))
            {
                // Dissociate old tasks then associate new tasks
                var removedTaskIds = existingUser.TaskIds.Except(updatedEntity.TaskIds).ToList();
                var addedTaskIds = updatedEntity.TaskIds.Except(existingUser.TaskIds).ToList();

                // Dissociate old tasks
                foreach (int i in removedTaskIds)
                {
                    TaskEntity? taskToUpdate = await _taskRepository.GetTaskByIdAsync(i);
                    if (taskToUpdate != null)
                        await DissociateUserFromTask(existingUser.Id, taskToUpdate);
                    else
                        _logger.LogInformation($"Task with id '{i}' not found - ignored");
                }

                // Associate new tasks
                foreach (int i in addedTaskIds)
                {
                    TaskEntity? taskToUpdate = await _taskRepository.GetTaskByIdAsync(i);
                    if (taskToUpdate != null)
                        await AssociateUserToTask(existingUser.Id, taskToUpdate);
                    else
                    {
                        _logger.LogWarning($"Task with id '{i}' does not exist - has been removed from user tasks list.");
                        updatedEntity.TaskIds.Remove(i);
                    }
                }
            }

            // Update existingUser with updatedUser data
            await _userRepository.UpdateUserAsync(existingUser, updatedEntity);

            return UserMapper.ToDto(updatedEntity);
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

        /// <summary>
        /// Associate a user ID to a task
        /// </summary>
        /// <param name="userId">User ID to associate</param>
        /// <param name="task">Task to update</param>
        /// <returns></returns>
        private async Task AssociateUserToTask(int userId, TaskEntity task)
        {
            task.UserId = userId;
            task.UpdatedAt = DateTime.UtcNow;
            task.UpdatedBy = userId.ToString();
            await _taskRepository.SaveUpdatesAsync(task);
        }

        /// <summary>
        /// Dissociate a user ID from a task
        /// </summary>
        /// <param name="userId">Old usere ID for updates handling</param>
        /// <param name="task">Task to update</param>
        /// <returns></returns>
        private async Task DissociateUserFromTask(int userId, TaskEntity task)
        {
            task.UserId = 0;
            task.UpdatedAt = DateTime.UtcNow;
            task.UpdatedBy = userId.ToString();
            await _taskRepository.SaveUpdatesAsync(task);
        }
    }
}
