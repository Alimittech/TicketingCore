using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserDependency.Commands.CreateUserDependency;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.FindUserDependency;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetOpunitManager;
using Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetUserDependency;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserDependencyFacad
    {
        IFindService<ResultFindUserDependencyDto, int> FindUserDependencyWithIdService { get; }
        IFindService<ResultFindUserDependencyDto, int> FindUserDependencyWithUserIdService { get; }
        IGetService<ResultGetUserDepDto, int> GetUserDepWithUserIdService { get; }
        IGetService<string, RequestUserDependencyDto> GetOpunitManagerService { get; }
        ICreateService<int?, RequestCreateUserDependencyDto> CreateUserDependencyService { get; }
        IDeleteService DeleteUserDependencyService { get; }
    }
}
