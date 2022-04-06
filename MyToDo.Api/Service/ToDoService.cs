using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;

namespace MyToDo.Api.Service
{
    /// <summary>
    /// 待办事项服务实现
    /// </summary>
    public class ToDoService:IToDoService
    {
        private readonly IUnitOfWork _work;
        private readonly IRepository<ToDo> _repository;
        public ToDoService(IUnitOfWork work)
        {
            _work = work;
            _repository = _work.GetRepository<ToDo>();
        }
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var todos = await _repository.GetAllAsync();
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

        public async Task<ApiResponse> AddAsync(ToDo model)
        {
            try
            {
                await _repository.InsertAsync(model);
                if (await _work.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model);
                else return new ApiResponse("添加数据失败");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        public async Task<ApiResponse> UpdateAsync(ToDo model)
        {
            try
            {
                var todo = await _repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(model.Id));
                todo.Title = model.Title;
                todo.Content = model.Content;
                todo.Status = model.Status;
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
}
