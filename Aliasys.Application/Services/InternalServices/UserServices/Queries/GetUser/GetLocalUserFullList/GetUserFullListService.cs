using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserFullList
{
    public class GetUserFullListService : IGetService<ResultGetUserFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetUserFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetUserFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var userList = _context.Users.AsNoTracking().AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    userList = userList.Where(p => p.UserName.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                || p.DisplayName.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                || p.Email.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                                || p.PhoneNumber.Contains(request.Parameter.SearchKey.Trim())
                                                || p.ExtentionNumber.Contains(request.Parameter.SearchKey.Trim()));
                }

                int rowCount = 0;
                var finalUserList = userList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                    .Select(p => new RequestGetUserFullListDto
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        DisplayName = p.DisplayName,
                        Email = p.Email,
                        PhoneNumber = p.PhoneNumber,
                        ExtentionNumber = p.ExtentionNumber,
                        IsActive = p.IsActive
                    }).ToList();

                if (finalUserList != null)
                {
                    return new ResultDto<ResultGetUserFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetUserFullListDto
                        {
                            userList = finalUserList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize,
                        }
                    };
                }
                return new ResultDto<ResultGetUserFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetUserFullListDto
                    {
                        userList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.Page,
                        PageSize = request.Parameter.PageSize,
                    }
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetUserFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserFullListDto>
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
                    return new ResultDto<ResultGetUserFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
