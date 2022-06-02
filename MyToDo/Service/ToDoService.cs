using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Service;

public class ToDoService:BaseService<ToDoDto>,IToDoService
{
    private readonly HttpRestClient _client;

    public ToDoService(HttpRestClient client):base(client, "ToDo")
    {
        _client = client;
    }

    public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter)
    {
        BaseRequest request = new()
        {
            Method = RestSharp.Method.Get,
            Route = $"api/ToDo/GetAllFilter?PageIndex={parameter.PageIndex}&PageSize={parameter.PageSize}&Search={parameter.Search}&Status={parameter.Status}"
        };
        return await _client.ExecuteAsync<PagedList<ToDoDto>>(request);
    }

    public async Task<ApiResponse<SummaryDto>> SummaryAsync()
    {
        BaseRequest request = new()
        {
            Method = RestSharp.Method.Get,
            Route = $"api/ToDo/Summary"
        };
        return await _client.ExecuteAsync<SummaryDto>(request);
    }
}