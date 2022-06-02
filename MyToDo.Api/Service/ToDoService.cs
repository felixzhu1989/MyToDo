using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;

namespace MyToDo.Api.Service;

/// <summary>
/// 待办事项服务实现
/// </summary>
public class ToDoService : IToDoService
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IRepository<ToDo> _repository;
    public ToDoService(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
        _repository = _work.GetRepository<ToDo>();
    }
    public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
    {
        try
        {
            var todos = await _repository.GetPagedListAsync(
                t => string.IsNullOrWhiteSpace(parameter.Search) || t.Title.Contains(parameter.Search),
                pageIndex: parameter.PageIndex,
                pageSize: parameter.PageSize,
                orderBy: source => source.OrderByDescending(x => x.CreateDate));
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
            var todo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
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
                return new ApiResponse(true, todo);
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
            var todo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
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

    public async Task<ApiResponse> GetAllFilterAsync(ToDoParameter parameter)
    {
        try
        {
            var todos = await _repository.GetPagedListAsync(
                t => (string.IsNullOrWhiteSpace(parameter.Search) || t.Title.Contains(parameter.Search))&&(parameter.Status==null || t.Status.Equals(parameter.Status)),
                pageIndex: parameter.PageIndex,
                pageSize: parameter.PageSize,
                orderBy: source => source.OrderByDescending(x => x.CreateDate));
            return new ApiResponse(true, todos);
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> Summary()
    {
        try
        {
            //待办事项结果，按照创建时间排序
            var toDos = await _work.GetRepository<ToDo>().GetAllAsync(
                orderBy: source => source.OrderByDescending(t => t.CreateDate));
            //备忘结果，按照创建时间排序
            var memos = await _work.GetRepository<Memo>().GetAllAsync(
                orderBy: source => source.OrderByDescending(t => t.CreateDate));
            SummaryDto summary = new();
            summary.Sum=toDos.Count();//汇总待办事项数量
            summary.CompletedCount=toDos.Where(t=>t.Status==1).Count();//统计完成待办事项数量
            summary.CompletedRatio=(summary.CompletedCount/(double)summary.Sum).ToString("0%");//完成率
            summary.MemoCount=memos.Count();//汇总备忘录数量
            summary.ToDoDtos=new ObservableCollection<ToDoDto>(_mapper.Map<List<ToDoDto>>(toDos.Where(t => t.Status==0)));
            summary.MemoDtos=new ObservableCollection<MemoDto>(_mapper.Map<List<MemoDto>>(memos));

            return new ApiResponse(true,summary);
        }
        catch (Exception ex)
        {
            return new ApiResponse(false, "");
        }
    }
}