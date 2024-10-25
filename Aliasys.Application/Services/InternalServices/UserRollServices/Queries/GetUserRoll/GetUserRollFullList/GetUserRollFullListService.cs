using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollFullList
{
    public class GetUserRollFullListService : IGetService<ResultGetUserRollFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetUserRollFullListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetUserRollFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var rollList = _context.UserRolls.AsNoTracking().AsQueryable();
                int rowCount = 0;

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    rollList = rollList.Where(p => p.RollName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalRollList = rollList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount).Select(p => new RequestGetUserRollFullListDto
                {
                    Id = p.Id,
                    Name = p.RollName,
                    Description = p.Description,
                }).ToList();
                if (finalRollList.Any())
                {
                    return new ResultDto<ResultGetUserRollFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetUserRollFullListDto
                        {
                            rollList = finalRollList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetUserRollFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetUserRollFullListDto
                    {
                        rollList = null,
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
                    return new ResultDto<ResultGetUserRollFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserRollFullListDto>
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
                    return new ResultDto<ResultGetUserRollFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserRollFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
