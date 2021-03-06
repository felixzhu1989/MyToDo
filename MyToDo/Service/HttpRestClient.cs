using System;
using System.Threading.Tasks;
using MyToDo.Shared;
using Newtonsoft.Json;
using RestSharp;
namespace MyToDo.Service;
public class HttpRestClient
{
    private readonly string _apiUrl;
    protected readonly RestClient _client;
    public HttpRestClient(string apiUrl)
    {
        _apiUrl=apiUrl;
        _client=new RestClient();
    }

    public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
    {
        var resource = new Uri($"{_apiUrl}{baseRequest.Route}");
        var request = new RestRequest(resource, baseRequest.Method);
        request.AddHeader("Content-Type", baseRequest.ContentType);
        //传递的参数
        if (baseRequest.Parameter != null) request.AddJsonBody(baseRequest.Parameter);
        var response = await _client.ExecuteAsync(request);
        if (response.StatusCode==System.Net.HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content!)!;        
        else        
            return new ApiResponse(response.ErrorMessage!); 
    }
    public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
    {
        var resource = new Uri($"{_apiUrl}{baseRequest.Route}");
        var request = new RestRequest(resource, baseRequest.Method);
        request.AddHeader("Content-Type", baseRequest.ContentType);
        //传递的参数
        if (baseRequest.Parameter != null) request.AddJsonBody(baseRequest.Parameter);

        var response = await _client.ExecuteAsync(request);
        if (response.StatusCode==System.Net.HttpStatusCode.OK)
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content!)!;
        else
            return new ApiResponse<T>() {Message=response.ErrorMessage,Status=false };
    }
}