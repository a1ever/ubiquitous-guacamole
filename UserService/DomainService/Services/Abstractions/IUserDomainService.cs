using UserService.DomainService.Domain;
using UserService.DomainService.DTOs;

namespace UserService.DomainService.Services.Abstractions;

public interface IUserDomainService
{
    Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto dto);
    Task<User?> GetUserByIdAsync(int id);
    Task<IEnumerable<User>> GetUsersByNameAsync(string name, string surname);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}