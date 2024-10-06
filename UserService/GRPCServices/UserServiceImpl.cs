using Grpc.Core;
using UserService.DomainService.DTOs;
using UserService.DomainService.Services.Abstractions;

namespace UserService.GRPCServices;

public class UserServiceImpl : UserService.UserServiceBase
{
    private readonly IUserDomainService _userDomainService;
    private readonly Mapper.Mapper _mapper;

    public UserServiceImpl(IUserDomainService userDomainService, Mapper.Mapper mapper)
    {
        _userDomainService = userDomainService;
        _mapper = mapper;
    }

    public override async Task<UserDataReply> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        UserRequestDto userToCreate = _mapper.ToUserRequestDto(request);
        
        UserResponseDto result = await _userDomainService.CreateUserAsync(userToCreate);

        return _mapper.ToUserDataReply(result);
    }

    public override async Task<UserDataReply> GetById(GetByIdRequest request, ServerCallContext context)
    {
        UserResponseDto? result = await _userDomainService.GetUserByIdAsync(request.Id);

        if (result == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No user with such id"));
        }

        return _mapper.ToUserDataReply(result);
    }

    public override async Task<UsersDataReply> GetByName(GetByNameRequest request, ServerCallContext context)
    {
        IEnumerable<UserResponseDto>? result = await _userDomainService.GetUsersByNameAsync(request.Name, request.Surname);

        if (result == null || !result.Any())
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No user with such name"));
        }

        UsersDataReply reply = new UsersDataReply();
        
        foreach (var user in result)
        {
            reply.Users.Add(_mapper.ToUserDataReply(user));
        }

        return reply;
    }

    public override async Task<SuccessReply> Update(UpdateRequest request, ServerCallContext context)
    {
        UserRequestDto userToUpdate = _mapper.ToUserRequestDto(request);

        bool result = await _userDomainService.UpdateUserAsync(userToUpdate);
        
        return new SuccessReply { Response = result };
    }

    public override async Task<SuccessReply> Delete(DeleteRequest request, ServerCallContext context)
    {
        bool result = await _userDomainService.DeleteUserAsync(request.Id);
        
        return new SuccessReply { Response = result };
    }
}