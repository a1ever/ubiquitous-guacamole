using UserService.DomainService.Domain;
using UserService.DomainService.Repositories.Abstractions;
using UserService.DomainService.Services.Abstractions;
using UserService.DomainService.DTOs;
using UserService.DomainService.Mappings;

namespace UserService.DomainService.Services.Implementations;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _userRepository;
    private readonly UserMapper _userMapper;

    public UserDomainService(IUserRepository userRepository, UserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<UserResponseDto> CreateUserAsync(UserRequestDto dto)
    {
        // Маппинг из DTO в доменную модель
        var user = _userMapper.ToDomain(dto);

        // Проверка уникальности логина
        var existingUser = await _userRepository.GetUserByLoginAsync(user.Login);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Пользователь с таким логином уже существует.");
        }

        // Создание пользователя
        bool wasUserCreated = await _userRepository.CreateUserAsync(user);

        if (!wasUserCreated)
        {
            throw new InvalidOperationException("Не удалось создать пользователя");
        }

        // Маппинг из доменной модели в DTO для ответа
        return _userMapper.ToDto(user);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        User? user = await _userRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        return _userMapper.ToDto(user);
    }

    public async Task<IEnumerable<User>> GetUsersByNameAsync(string name, string surname)
    {
        return await _userRepository.GetUsersByNameAsync(name, surname);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        // Проверка существования пользователя
        var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("Пользователь не найден.");
        }

        // Предотвращаем изменение логина
        user.Login = existingUser.Login;

        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}