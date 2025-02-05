using Grpc.Net.Client;
using UserService;
using UserService.Services;

public class UserServiceClient
{
    private readonly UserProtoService.UserProtoServiceClient _client;

    public UserServiceClient(UserProtoService.UserProtoServiceClient client)
    {
        _client = client;
    }

    public async Task<UserResponse> GetUserInfoAsync(string userId)
    {
        return await _client.GetUserInfoAsync(new UserRequest { UserId = userId });
    }
}
