using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service;

public interface ILoginService
{
    Task<ApiResponse> LoginAsync(string account,string password);
    Task<ApiResponse> RegisterAsync(UserDto user);
}