using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.PositionServices.Queries.GetPosition.GetPositionFullList
{
    public class GetPositionFullListService : IGetService<ResultGetPositionFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetPositionFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetPositionFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var posList = _context.Positions.AsNoTracking().AsQueryable();
                int rowCount = 0;

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    posList = posList.Where(p => p.Name.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalPositionList = posList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount).Select(p => new RequestGetPositionFullListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();
                if (finalPositionList.Any())
                {
                    return new ResultDto<ResultGetPositionFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetPositionFullListDto
                        {
                            positionList = finalPositionList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetPositionFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetPositionFullListDto
                    {
                        positionList = null,
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
                    return new ResultDto<ResultGetPositionFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetPositionFullListDto>
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
                    return new ResultDto<ResultGetPositionFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetPositionFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
