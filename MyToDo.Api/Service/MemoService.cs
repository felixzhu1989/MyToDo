using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Service;
/// <summary>
/// 备忘录服务实现
/// </summary>
public class MemoService:IMemoService
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IRepository<Memo> _repository;
    public MemoService(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
        _repository = _work.GetRepository<Memo>();
    }
    public async Task<ApiResponse> GetAllAsync(QueryParameter parameter)
    {
        try
        {
            var memos = await _repository.GetPagedListAsync(predicate: t => string.IsNullOrWhiteSpace(parameter.Search) ? true : t.Title.Equals(parameter.Search), pageIndex: parameter.PageIndex, pageSize: parameter.PageSize, orderBy: source => source.OrderByDescending(x => x.CreateDate));
            return new ApiResponse(true, memos);
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
            var memo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
            return new ApiResponse(true, memo);
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> AddAsync(MemoDto model)
    {
        try
        {
            var memo = _mapper.Map<Memo>(model);
            await _repository.InsertAsync(memo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, model);
            else return new ApiResponse("添加数据失败");
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> UpdateAsync(MemoDto model)
    {
        try
        {
            var dbMemo = _mapper.Map<Memo>(model);
            var memo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(dbMemo.Id));
            memo.Title = dbMemo.Title;
            memo.Content = dbMemo.Content;

            memo.UpdateDate=DateTime.Now;
            _repository.Update(memo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, memo);
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
            var memo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
            _repository.Delete(memo);
            if (await _work.SaveChangesAsync() > 0)
                return new ApiResponse(true, memo);
            else return new ApiResponse("删除数据失败");
        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }
}