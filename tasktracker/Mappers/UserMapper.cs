using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Common;

namespace tasktracker.Mappers
{
    /// <summary>
    /// Mapper management for Users
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Mapper CreateUserDto -> UserEntity
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>UserEntity object</returns>
        public static UserEntity ToCreateEntity(CreateUserDto dto)
        {
            return new UserEntity
            {
                Name = dto.Name,
                Firstname = dto.Firstname,
                Email = dto.Email,
                Phone = dto.Phone,
                Role = dto.Role,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "0",
                UpdatedBy = dto.UpdatedBy ?? "0"
            };
        }

        /// <summary>
        /// Mapper UpdateUserDto -> UserEntity
        /// </summary>
        /// <param name="existingUser">Existing user in DB</param>
        /// <param name="updatedUser">New data to set</param>
        /// <returns>UserEntity object</returns>
        public static UserEntity ToUpdateUser(UserEntity existingUser, UpdateUserDto updatedUser)
        {
            string LocalPasswordHash = updatedUser.Password != null ? PasswordHelper.HashPassword(updatedUser.Password) : existingUser.PasswordHash;
            return new UserEntity
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Firstname = existingUser.Firstname,
                Email = updatedUser.Email ?? existingUser.Email,
                Phone = updatedUser.Phone ?? existingUser.Phone,
                Role = updatedUser.Role ?? existingUser.Role,
                PasswordHash = LocalPasswordHash,
                TaskIds = updatedUser.TaskIds ?? existingUser.TaskIds,
                CreatedAt = existingUser.CreatedAt,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = existingUser.CreatedBy,
                UpdatedBy = updatedUser.UpdatedBy
            };
        }

        /// <summary>
        /// Mapper UserDto -> UserEntity
        /// </summary>
        /// <param name="dto">dto to map</param>
        /// <returns>UserEntity object</returns>
        public static UserEntity ToEntity(UserDto dto)
        {
            return new UserEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Firstname = dto.Firstname,
                Email = dto.Email,
                Phone = dto.Phone,
                Role = dto.Role,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                TaskIds = dto.TaskIds,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy
            };
        }

        /// <summary>
        /// Mapper UserEntity -> UserDto
        /// </summary>
        /// <param name="entity">entity to map</param>
        /// <returns>UserDto object</returns>
        public static UserDto ToDto(UserEntity entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Firstname = entity.Firstname,
                Email = entity.Email,
                Phone = entity.Phone,
                Role = entity.Role,
                Password = "",
                TaskIds = entity.TaskIds,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}
