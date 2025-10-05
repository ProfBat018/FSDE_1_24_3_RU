using Grpc.Core;
using UserService.Application.Interfaces;
using UserService.Grpc;

namespace UserService.API.Grpc;

public class UserGrpcService : UserGrpc.UserGrpcBase
{
    private readonly IUserService _userService;

    public UserGrpcService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<UserResponse> GetUserByEmail(UserEmailRequest request, ServerCallContext context)
    {
        var result = await _userService.GetByEmailAsync(request.Email);

        if (!result.IsSuccess || result.Data == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, result.Message));
        }

        var user = result.Data;

        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
    }
}