using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using System.Threading.Tasks;
namespace MyToDo.Service;
public class LoginService : ILoginService
{
    private readonly HttpRestClient _client;
    private readonly string serviceName = "Login";
    public LoginService(HttpRestClient client)
    {
        _client = client;
    }
    public async Task<ApiResponse<UserDto>> LoginAsync(UserDto user)
    {
        BaseRequest request = new()
        {
            Method=RestSharp.Method.Post,
            Route=$"api/{serviceName}/Login",
            Parameter=user
        };
        return await _client.ExecuteAsync<UserDto>(request);
    }

    public async Task<ApiResponse<UserDto>> RegisterAsync(UserDto user)
    {
        BaseRequest request = new()
        {
            Method=RestSharp.Method.Post,
            Route=$"api/{serviceName}/Register",
            Parameter=user
        };
        return await _client.ExecuteAsync<UserDto>(request);
    }
}
