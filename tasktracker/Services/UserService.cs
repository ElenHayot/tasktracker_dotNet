using tasktracker.DtoModels;
using tasktracker.Entities;
using tasktracker.Enums;
using tasktracker.Mappers;
using tasktracker.Repositories;
using tasktracker.Exceptions;

namespace tasktracker.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            UserEntity userEntity = UserMapper.ToCreateEntity(userDto);
            UserEntity createdUser = await _userRepository.CreateUserAsync(userEntity);
            UserDto user = UserMapper.ToDto(createdUser);

            return user;
        }

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

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            UserEntity? user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new NotFoundException($"User with email '{email}' not found.");
            }
            
            return UserMapper.ToDto(user);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"No user with id '{id}' found.");
            }

            return UserMapper.ToDto(user);
        }
    }
}
