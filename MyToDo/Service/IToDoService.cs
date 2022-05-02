using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Service;

public interface IToDoService:IBaseService<ToDoDto>
{
    Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter);
}