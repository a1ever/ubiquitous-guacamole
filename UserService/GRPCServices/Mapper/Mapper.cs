using UserService.DomainService.DTOs;

namespace UserService.GRPCServices.Mapper;

public class Mapper
{
    public UserRequestDto ToUserRequestDto(CreateUserRequest request) => new UserRequestDto
    {
        Login = request.Login,
        Age = request.Age,
        Name = request.Name,
        Surname = request.Surname,
        Password = request.Password
    };

    public UserRequestDto ToUserRequestDto(UpdateRequest request) => new UserRequestDto
    {
        Login = request.Login,
        Password = request.Password == "" ? null : request.Password,
        Name = request.Name == "" ? null : request.Name,
        Surname = request.Surname == "" ? null : request.Surname,
        Age = request.Age
    };




    public UserDataReply ToUserDataReply(UserResponseDto user) => new UserDataReply
    {
        Id = user.Id,
        Login = user.Login,
        Name = user.Name,
        Surname = user.Surname,
        Age = user.Age
    };
}