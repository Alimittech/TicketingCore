using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.CreateUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.UpdateUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.FindUserGroup;
using Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.GetUserGroup.GetUserGroupFullList;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserGroupFacad
    {
        IFindService<ResultFindUserGroupDto, int> FindUserGroupWithIdService {  get; }
        IGetService<List<SelectListItem>> GetUserGroupSelectListService {  get; }
        IGetService<ResultGetUserGroupFullListDto, PaginationDto> GetUserGroupFullListService { get; }
        ICreateService<int?, RequestCreateUserGroupDto> CreateUserGroupService { get; }
        IUpdateService<int?, RequestUpdateUserGroupDto> UpdateUserGroupService { get; }
        IDeleteService DeleteUserGroupService { get; }
    }
}
