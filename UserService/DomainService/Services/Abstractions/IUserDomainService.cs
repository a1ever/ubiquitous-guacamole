using UserService.DomainService.Domain;

namespace UserService.DomainService.Services.Abstractions;

public interface IUserDomainService
{
    Task<User> CreateUserAsync(User user);
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetUsersByNameAsync(string name, string surname);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}