using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemFullList
{
    public class GetSystemFullListService : IGetService<ResultGetSystemFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetSystemFullListService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetSystemFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var systems = _context.SystemComponents.AsNoTracking().ToList();
                int rowCount = 0;
                if (systems.Any())
                {
                    var sysList = systems.Join(systems, sys => sys.ParentSystemId, childSys => childSys.Id,
                                        (sys, childSys) => new
                                        {
                                            sysId = sys.Id,
                                            sysParemt = childSys.Name,
                                            sysName = sys.Name,
                                            sysDescription = sys.Description,
                                        }).AsQueryable();
                    if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                    {
                        sysList = sysList.Where(p => p.sysName.Contains(request.Parameter.SearchKey.Trim().ToLower())

                                                || p.sysDescription.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                    }
                    var finalSysList = sysList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                        .Select(p => new RequestGetSystemFullListDto
                        {
                            Id = p.sysId,
                            ParentSystem = p.sysParemt,
                            Name = p.sysName,
                            Description = p.sysDescription,
                        }).ToList();

                    return new ResultDto<ResultGetSystemFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetSystemFullListDto
                        {
                            sysList = finalSysList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize,
                        }
                    };
                }
                return new ResultDto<ResultGetSystemFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no items",
                    Data = new ResultGetSystemFullListDto
                    {
                        sysList = null,
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
                    return new ResultDto<ResultGetSystemFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetSystemFullListDto>
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
                    return new ResultDto<ResultGetSystemFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetSystemFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
