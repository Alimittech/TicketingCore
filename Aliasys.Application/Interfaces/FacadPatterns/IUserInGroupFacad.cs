using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.IServices.InternalServices;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Commands.CreateUserInGroup;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.FindUserInGroup;
using Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList.GetUserInGroupWithGrpName;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserInGroupFacad
    {
        IFindService<ResultFindUserInGroupDto, int> FindUserInGroupWithIdService {  get; }
        IFindService<List<ResultFindUserInGroupDto>, int> FindUserInGroupWithUserIdService { get; }
        IGetService<List<SelectListItem>, int> GetUserInGroupSelectListService { get; }
        IGetService<List<SelectListItem>, string> GetUserInGroupSelectListWithGrpNameService { get; }
        IGetService<List<int>, int> GetUserInGroupWithUserIdService { get; }
        ICreateService<int?, RequestCreateUserInGroupDto> CreateUserInGroupService { get; }
        IDeleteService DeleteUserInGroupService { get; }
        IUserInGroupService UserInGroupService { get; }
    }
}
