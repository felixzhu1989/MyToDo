using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public async Task<ApiResponse> LoginAsync(UserDto user)
    {     
        BaseRequest request = new BaseRequest();
        request.Method=RestSharp.Method.Post;
        request.Route=$"api/{serviceName}/Login";
        request.Parameter=user;
        return await _client.ExecuteAsync(request);
    }

    public async Task<ApiResponse> RegisterAsync(UserDto user)
    {
        BaseRequest request = new BaseRequest();
        request.Method=RestSharp.Method.Post;
        request.Route=$"api/{serviceName}/Register";
        request.Parameter=user;
        return await _client.ExecuteAsync(request);
    }
}
