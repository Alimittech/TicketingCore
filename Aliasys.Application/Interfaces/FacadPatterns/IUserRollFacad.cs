using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.CreateUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.FindUserRoll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollAll;
using Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollFullList;
using Aliasys.Common.Dtos;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserRollFacad
    {
        IFindService<ResultFindUserRollDto, int> FindUserRollWithIdService { get; }
        IFindService<ResultFindUserRollDto, string> FindUserRollWithNameService { get; }
        IGetService<ResultGetUserRollFullListDto, PaginationDto> GetUserRollFullListService { get; }
        IGetService<List<ResultGetUserRollAllDto>> GetUserRollAllService { get; }
        ICreateService<int?, RequestCreateUserRollDto> CreateUserRollService { get; }
        IUpdateService<int?, RequestUpdateUserRollDto> UpdateUserRollService { get; }
        IDeleteService DeleteUserRollService { get; }
    }
}
