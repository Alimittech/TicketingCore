using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.ChangeStateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.CreateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.FindUser;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserDetails;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserFullList;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.ValidateLdapUser;
using Aliasys.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aliasys.Application.Interfaces.FacadPatterns
{
    public interface IUserFacad
    {
        IFindService<ResultFindDto, int> FindUserWithIdService { get; }
        IFindService<ResultFindDto, string> FindUserWithUserNameService { get; }
        IFindService<ResultFindDto, string> FindUserWithEmailService { get; }
        IGetService<ResultGetUserFullListDto, PaginationDto> GetUserFullListService { get; }
        IGetService<List<SelectListItem>> GetUserSelectListService { get; }
        IGetService<ResultGetUserDetailWithUserNameDto, string> GetUserDetailWithUserNameService { get; }
        IValidateLdapUser<RequestValidateLdapUserDto> ValidateLdapUser { get; }
        ICreateService<int?, RequestCreateUserDto> CreateUserService { get; }
        IUpdateService<int?, RequestUpdateUserDto> UpdateUserService { get; }
        IDeleteService DeleteUserService { get; }
        IChangeStateUserService ChangeStateUserService { get; }
    }
}
