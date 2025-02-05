using Grpc.Core;

namespace UserService.Services;

public class UserGrpcService : UserProtoService.UserProtoServiceBase
{
    public override Task<UserResponse> GetUserInfo(UserRequest request, ServerCallContext context)
    {
        // В реальном проекте здесь будет обращение к БД
        return Task.FromResult(new UserResponse
        {
            UserId = request.UserId,
            Name = "John Doe",
            Email = "johndoe@example.com"
        });
    }
}
