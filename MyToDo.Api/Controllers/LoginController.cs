using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class LoginController:ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ApiResponse> Login(string account, string password) =>
        await _service.LoginAsync(account, password);

    [HttpPost]
    public async Task<ApiResponse> Register([FromBody] UserDto user) => await _service.RegisterAsync(user);
}