using UserService.DomainService.Domain;
using UserService.DomainService.Repositories.Abstractions;
using UserService.DomainService.Services.Abstractions;

namespace UserService.DomainService.Services.Implementations;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Проверка уникальности логина
        var existingUser = await _userRepository.GetUserByLoginAsync(user.Login);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Пользователь с таким логином уже существует.");
        }

        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
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