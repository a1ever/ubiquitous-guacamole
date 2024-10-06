using UserService.DomainService.Domain;
using UserService.DomainService.DTOs;

namespace UserService.DomainService.Services.Abstractions;

public interface IUserDomainService
{
    Task<UserResponseDto> CreateUserAsync(UserRequestDto dto);
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task<IEnumerable<UserResponseDto>?> GetUsersByNameAsync(string name, string surname);
    Task<bool> UpdateUserAsync(UserRequestDto user);
    Task<bool> DeleteUserAsync(int id);
}