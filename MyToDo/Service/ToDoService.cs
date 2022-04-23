using MyToDo.Shared.Dtos;

namespace MyToDo.Service;

public class ToDoService:BaseService<ToDoDto>,IToDoService
{
    public ToDoService(HttpRestClient client):base(client, "ToDo")
    {
    }
}