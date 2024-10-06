using Riok.Mapperly.Abstractions;
using UserService.DomainService.Domain;
using UserService.DomainService.DTOs;

namespace UserService.DomainService.Mappings;

[Mapper]
public partial class UserMapper
{
    public partial User ToDomain(CreateUserRequestDto dto);
    public partial UserResponseDto ToDto(User user);
}
