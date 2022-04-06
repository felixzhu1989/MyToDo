using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Service;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TodoController:ControllerBase
{
    private readonly IToDoService _service;

    public TodoController(IToDoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ApiResponse> Get(int id) =>await _service.GetSingleAsync(id);

    [HttpGet]
    public async Task<ApiResponse> GetAll([FromQuery] QueryParameter param) => await _service.GetAllAsync(param);

    [HttpPost]
    public async Task<ApiResponse> Add([FromBody]ToDoDto model) => await _service.AddAsync(model);

    [HttpPost]
    public async Task<ApiResponse> Update([FromBody]ToDoDto model) => await _service.UpdateAsync(model);

    [HttpDelete]
    public async Task<ApiResponse> Delete(int id) => await _service.DeleteAsync(id);

}