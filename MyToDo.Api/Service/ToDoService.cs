using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service;

/// <summary>
/// 待办事项服务实现
/// </summary>
public class ToDoService:IToDoService
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IRepository<ToDo> _repository;
    public ToDoService(IUnitOfWork work,IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
        _repository = _work.GetRepository<ToDo>();
    }
    public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
    {
        try
        {
            var todos = await _repository.GetPagedListAsync(predicate:t=>string.IsNullOrWhiteSpace(parameter.Search)?true:t.Title.Equals(parameter.Search),pageIndex:parameter.PageIndex,pageSize:parameter.PageSize,orderBy:source=>source.OrderByDescending(x=>x.CreateDate));
            return new ApiResponse(true, todos);
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> GetSingleAsync(int id)
    {
        try
        {
            var todo = await _repository.GetFirstOrDefaultAsync(predicate:t=>t.Id.Equals(id));
            return new ApiResponse(true, todo);
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> AddAsync(ToDoDto model)
    {
        try
        {
            var todo = _mapper.Map<ToDo>(model);
            await _repository.InsertAsync(todo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, model);
            else return new ApiResponse("添加数据失败");
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> UpdateAsync(ToDoDto model)
    {
        try
        {
            var dbTodo = _mapper.Map<ToDo>(model);
            var todo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(dbTodo.Id));
            todo.Title = dbTodo.Title;
            todo.Content = dbTodo.Content;
            todo.Status = dbTodo.Status;
            todo.UpdateDate=DateTime.Now;
            _repository.Update(todo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, todo);
            else return new ApiResponse("更新数据失败");
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> DeleteAsync(int id)
    {
        try
        {
            var todo= await _repository.GetFirstOrDefaultAsync(predicate:t=>t.Id.Equals(id));
            _repository.Delete(todo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, todo);
            else return new ApiResponse("删除数据失败");
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }
}