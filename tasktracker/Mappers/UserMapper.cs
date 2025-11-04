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
                Role = dto.Role,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = "0",
                UpdatedBy = "0"
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
                Role = dto.Role,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = dto.Id.ToString()
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
                Role = entity.Role,
                Password = "",
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}
