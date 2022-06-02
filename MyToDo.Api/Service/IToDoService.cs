using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service;

public interface IToDoService:IBaseService<ToDoDto>
{
    Task<ApiResponse> GetAllFilterAsync(ToDoParameter parameter);
    Task<ApiResponse> Summary();
}