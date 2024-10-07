using System.ComponentModel.DataAnnotations;
using FluentValidation;
using UserService.DomainService.Domain;
using UserService.DomainService.DTOs;
using UserService.DomainService.Repositories.Abstractions;
using UserService.DomainService.Services.Abstractions;
using UserService.DomainService.Mappings;
using ValidationException = FluentValidation.ValidationException;

namespace UserService.DomainService.Services.Implementations;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _userRepository;
    private readonly UserMapper _userMapper;
    private readonly IValidator<UserRequestDto> _createUserValidator;
    private readonly IValidator<UserRequestDto> _updateUserValidator;

    public UserDomainService(
        IUserRepository userRepository,
        UserMapper userMapper,
        IValidator<UserRequestDto> createUserValidator,
        IValidator<UserRequestDto> updateUserValidator)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _createUserValidator = createUserValidator;
        _updateUserValidator = updateUserValidator;
    }

    public async Task<UserResponseDto> CreateUserAsync(UserRequestDto dto)
    {
        // Валидация входящих данных при создании
        var validationResult = _createUserValidator.Validate(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
            throw new InvalidOperationException("Не удалось создать пользователя.");
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

    public async Task<IEnumerable<UserResponseDto>?> GetUsersByNameAsync(string name, string surname)
    {
        IEnumerable<User> users = await _userRepository.GetUsersByNameAsync(name, surname);

        if (users == null || !users.Any())
        {
            return null;
        }
        
        IEnumerable<UserResponseDto> usersDto = new List<UserResponseDto>();

        foreach (var user in users)
        {
            usersDto.Append(_userMapper.ToDto(user));
        }
        
        return usersDto;
    }

    public async Task<bool> UpdateUserAsync(UserRequestDto user)
    {
        // Валидация входящих данных при обновлении
        var validationResult = _updateUserValidator.Validate(user);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var existingUser = await _userRepository.GetUserByLoginAsync(user.Login);
        if (existingUser == null)
        {
            throw new InvalidOperationException("Пользователь не найден.");
        }

        // Обновляем только те поля, которые не null
        var userToUpdate = new User
        {
            Id = existingUser.Id,
            Login = existingUser.Login,
            Password = user.Password ?? existingUser.Password,
            Name = user.Name ?? existingUser.Name,
            Surname = user.Surname ?? existingUser.Surname,
            Age = user.Age ?? existingUser.Age
        };

        // Выполняем обновление пользователя в базе данных
        return await _userRepository.UpdateUserAsync(userToUpdate);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
}