using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;
using MyToDo.Api.Service;

namespace MyToDo.Api.Controllers
{
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
        public async Task<ApiResponse> GetAll() => await _service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody]ToDo model) => await _service.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody]ToDo model) => await _service.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await _service.DeleteAsync(id);

    }
}
