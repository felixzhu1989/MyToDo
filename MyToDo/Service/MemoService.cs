using MyToDo.Shared.Dtos;

namespace MyToDo.Service;

public class MemoService:BaseService<MemoDto>,IMemoService
{
    public MemoService(HttpRestClient client):base(client,"Memo")
    {
    }
}