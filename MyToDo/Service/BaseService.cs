using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Parameters;

namespace MyToDo.Service;

public class BaseService<TEntity>:IBaseService<TEntity> where TEntity:class
{
    private readonly HttpRestClient _client;
    private readonly string _serviceName;

    public BaseService(HttpRestClient client,string serviceName)
    {
        _client = client;
        _serviceName = serviceName;
    }

    public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
    {
        BaseRequest request = new BaseRequest
        {
            Method = RestSharp.Method.Post,
            Route = $"api/{_serviceName}/Add",
            Parameter = entity
        };
        return await _client.ExecuteAsync<TEntity>(request);
    }

    public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
    {
        BaseRequest request = new BaseRequest
        {
            Method = RestSharp.Method.Post,
            Route = $"api/{_serviceName}/Update",
            Parameter = entity
        };
        return await _client.ExecuteAsync<TEntity>(request);
    }

    public async Task<ApiResponse<TEntity>> DeleteAsync(int id)
    {
        BaseRequest request = new BaseRequest
        {
            Method = RestSharp.Method.Delete,
            Route = $"api/{_serviceName}/Delete?id={id}"
        };
        return await _client.ExecuteAsync<TEntity>(request);
    }

    public async Task<ApiResponse<TEntity>> GetFirstOrDefault(int id)
    {
        BaseRequest request = new BaseRequest
        {
            Method = RestSharp.Method.Get,
            Route = $"api/{_serviceName}/Get?id={id}"
        };
        return await _client.ExecuteAsync<TEntity>(request);
    }

    public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
    {
        BaseRequest request = new BaseRequest
        {
            Method = RestSharp.Method.Get,
            Route = $"api/{_serviceName}/GetAll?PageIndex={parameter.PageIndex}&PageSize={parameter.PageSize}&Search={parameter.Search}"
        };
        return await _client.ExecuteAsync<PagedList<TEntity>>(request);
    }
}