using UserService.DomainService.Domain;

namespace UserService.DomainService.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByLoginAsync(string login);
    Task<IEnumerable<User>> GetUsersByNameAsync(string name, string surname);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}