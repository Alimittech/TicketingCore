using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupFullList
{
    public class GetUserInGroupFullListService : IGetService<ResultGetUserInGroupFullListDto, RequestGetUserInGroupFullListDto>
    {
        private readonly IDataBaseContext _context;
        public GetUserInGroupFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetUserInGroupFullListDto> Get(RequestDto<RequestGetUserInGroupFullListDto> request)
        {
            try
            {
                var userInGroups = _context.UserInGroups.AsNoTracking().Where(p => p.UserGroupId_FK == request.Parameter.UserGroupId).ToList();
                var groups = _context.UserGroups.AsNoTracking().Where(p => p.Id == request.Parameter.UserGroupId).ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var result = userInGroups.Join(groups, userInGrp => userInGrp.UserGroupId_FK, grp => grp.Id,
                                            (userInGrp, grp) => new { userInGrp, grp })
                                         .Join(users, uig => uig.userInGrp.UserId_FK, usr => usr.Id,
                                             (uig, usr) => new
                                             {
                                                 finalId = uig.userInGrp.Id,
                                                 finalGroupName = uig.grp.GroupName,
                                                 finalUserDisplayName = usr.DisplayName,
                                                 finalUserUserName = usr.UserName,
                                             }).AsQueryable();
                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.PaginationDto.SearchKey))
                {
                    result = result.Where(p => p.finalGroupName.Contains(request.Parameter.PaginationDto.SearchKey.Trim().ToLower())
                                            || p.finalUserDisplayName.Contains(request.Parameter.PaginationDto.SearchKey.Trim().ToLower())
                                            || p.finalUserUserName.Contains(request.Parameter.PaginationDto.SearchKey.Trim().ToLower()));
                }
                var finalResult = result.ToPaged(request.Parameter.PaginationDto.Page, request.Parameter.PaginationDto.PageSize, out rowCount)
                    .Select(p => new GetUserInGroupFullListDto
                    {
                        Id = p.finalId,
                        GrpGroupName = p.finalGroupName,
                        UsrDisplayName = p.finalUserDisplayName,
                        UsrUserName = p.finalUserUserName,
                    }).ToList();
                if (finalResult.Any())
                {
                    return new ResultDto<ResultGetUserInGroupFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetUserInGroupFullListDto
                        {
                            userInGroupList = finalResult,
                            RowsCount = rowCount,
                            Page = request.Parameter.PaginationDto.Page,
                            PageSize = request.Parameter.PaginationDto.PageSize,
                        }
                    };
                }
                return new ResultDto<ResultGetUserInGroupFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Data = new ResultGetUserInGroupFullListDto
                    {
                        userInGroupList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.PaginationDto.Page,
                        PageSize = request.Parameter.PaginationDto.PageSize,
                    }
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetUserInGroupFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserInGroupFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "The application is not accessible!",
                };
            }
            catch (Exception ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetUserInGroupFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserInGroupFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
