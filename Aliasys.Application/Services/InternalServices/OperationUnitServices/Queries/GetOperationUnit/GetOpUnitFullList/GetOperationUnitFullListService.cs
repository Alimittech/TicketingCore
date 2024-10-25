using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.GetOperationUnit.GetOpUnitFullList
{
    public class GetOperationUnitFullListService : IGetService<ResultGetOperationUnitFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetOperationUnitFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetOperationUnitFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var opunitList = _context.OperationUnits.AsNoTracking().AsQueryable();
                int rowCount = 0;

                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    opunitList = opunitList.Where(p => p.Name.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalOpunitList = opunitList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount).Select(p => new RequestGetOperationUnitFullListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                }).ToList();
                if (finalOpunitList.Any())
                {
                    return new ResultDto<ResultGetOperationUnitFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetOperationUnitFullListDto
                        {
                            opunitList = finalOpunitList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize,
                        }
                    };
                }
                return new ResultDto<ResultGetOperationUnitFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetOperationUnitFullListDto
                    {
                        opunitList = null,
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
                    return new ResultDto<ResultGetOperationUnitFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOperationUnitFullListDto>
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
                    return new ResultDto<ResultGetOperationUnitFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOperationUnitFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
