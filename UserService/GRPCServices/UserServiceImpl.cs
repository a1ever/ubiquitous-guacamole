using Grpc.Core;
using UserService.DomainService.DTOs;
using UserService.DomainService.Services.Abstractions;

namespace UserService.GRPCServices;

public class UserServiceImpl : UserService.UserServiceBase
{
    private readonly IUserDomainService _userDomainService;

    public UserServiceImpl(IUserDomainService userDomainService)
    {
        _userDomainService = userDomainService;
    }

    public override async Task<UserDataReply> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        UserRequestDto userToCreate = new UserRequestDto
        {
            Id = null,
            Login = request.Login,
            Age = request.Age,
            Name = request.Name,
            Surname = request.Surname,
            Password = request.Password
        };
        
        UserResponseDto result = await _userDomainService.CreateUserAsync(userToCreate);

        if (result == null)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Cannot create such user"));
        }

        return new UserDataReply
        {
            Id = result.Id,
            Login = result.Login,
            Name = result.Name,
            Surname = result.Surname,
            Age = result.Age
        };
    }

    public override async Task<UserDataReply> GetById(GetByIdRequest request, ServerCallContext context)
    {
        UserResponseDto result = await _userDomainService.GetUserByIdAsync(request.Id);

        if (result == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No user with such id"));
        }

        return new UserDataReply
        {
            Id = result.Id,
            Login = result.Login,
            Name = result.Name,
            Surname = result.Surname,
            Age = result.Age
        };
    }

    public override async Task<UsersDataReply> GetByName(GetByNameRequest request, ServerCallContext context)
    {
        IEnumerable<UserResponseDto> result = await _userDomainService.GetUsersByNameAsync(request.Name, request.Surname);

        if (result == null || !result.Any())
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No user with such id"));
        }

        UsersDataReply reply = new UsersDataReply();
        
        foreach (var user in result)
        {
            reply.Users.Add(new UserDataReply
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age
            });
        }

        return reply;
    }

    public override async Task<SuccessReply> Update(UpdateRequest request, ServerCallContext context)
    {
        UserRequestDto userToUpdate = new UserRequestDto
        {
            Id = null,
            Login = request.Login,
            Age = request.Age,
            Name = request.Name,
            Surname = request.Surname,
            Password = request.Password
        };

        bool result = await _userDomainService.UpdateUserAsync(userToUpdate);
        return new SuccessReply { Response = result };
    }

    public override async Task<SuccessReply> Delete(DeleteRequest request, ServerCallContext context)
    {
        bool result = await _userDomainService.DeleteUserAsync(request.Id);
        return new SuccessReply { Response = result };
    }
}