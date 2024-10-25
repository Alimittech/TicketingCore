using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserGroupServices.Queries.GetUserGroup.GetUserGroupFullList
{
    public class GetUserGroupFullListService : IGetService<ResultGetUserGroupFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetUserGroupFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetUserGroupFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var userGroupList = _context.UserGroups.AsNoTracking().AsQueryable();
                int rowCount = 0;

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    userGroupList = userGroupList.Where(p => p.GroupName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalUserGroupList = userGroupList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount).Select(p => new RequestGetUserGroupFullListDto
                {
                    Id = p.Id,
                    GroupName = p.GroupName,
                }).ToList();
                if (finalUserGroupList.Any())
                {
                    return new ResultDto<ResultGetUserGroupFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetUserGroupFullListDto
                        {
                            userGroupList = finalUserGroupList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetUserGroupFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetUserGroupFullListDto
                    {
                        userGroupList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.Page,
                        PageSize = request.Parameter.PageSize
                    }
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetUserGroupFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserGroupFullListDto>
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
                    return new ResultDto<ResultGetUserGroupFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserGroupFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
