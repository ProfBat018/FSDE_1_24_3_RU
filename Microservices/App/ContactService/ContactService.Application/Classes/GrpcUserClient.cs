
using UserService.Grpc;

namespace ContactService.Application.Classes;

public class GrpcUserClient
{
    private readonly UserGrpc.UserGrpcClient _client;

    public GrpcUserClient(UserGrpc.UserGrpcClient client)
    {
        _client = client;
    }

    public async Task<UserResponse?> GetUserByEmailAsync(string email)
    {
        var request = new UserEmailRequest { Email = email };
        return await _client.GetUserByEmailAsync(request);
    }
}
