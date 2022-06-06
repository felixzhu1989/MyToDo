using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using System.Threading.Tasks;
namespace MyToDo.Service;
public interface ILoginService
{
    Task<ApiResponse> LoginAsync(UserDto user);
    Task<ApiResponse> RegisterAsync(UserDto user);
}
