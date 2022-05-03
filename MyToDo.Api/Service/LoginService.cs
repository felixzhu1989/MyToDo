using AutoMapper;
using MyToDo.Api.Context;
using MyToDo.Api.Context.UnitOfWork;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Service;
public class LoginService : ILoginService
{
    private readonly IUnitOfWork _work;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;
    public LoginService(IUnitOfWork work, IMapper mapper)
    {
        _work = work;
        _mapper = mapper;
        _repository = _work.GetRepository<User>();
    }
    public async Task<ApiResponse> LoginAsync(string account, string password)
    {
        try
        {
            var model = await _repository.GetFirstOrDefaultAsync(predicate: u =>
                  u.Account.Equals(account) && u.Password.Equals(password));
            if (model == null)
                return new ApiResponse("账号或密码错误，请重试");
            else return new ApiResponse(true, model);

        }
        catch (Exception e)
        {
            return new ApiResponse(e.Message);
        }
    }

    public async Task<ApiResponse> RegisterAsync(UserDto user)
    {
        try
        {
            var model = _mapper.Map<User>(user);

            var dbUser = await _repository.GetFirstOrDefaultAsync(predicate: u =>
                u.Account.Equals(model.Account));
            if (dbUser != null)
                return new ApiResponse($"账号:{model.Account}已存在，请重新注册");
            model.CreateDate=DateTime.Now;
            await _repository.InsertAsync(model);
            if (await _work.SaveChangesAsync()>0)
                return new ApiResponse(true, model);
            else return new ApiResponse("注册失败，请重新注册");
        }
        catch (Exception e)
        {
            return new ApiResponse($"注册失败，{e.Message}");
        }
    }
}