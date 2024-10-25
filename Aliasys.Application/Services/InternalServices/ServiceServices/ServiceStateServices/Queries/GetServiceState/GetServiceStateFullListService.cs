using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState
{
    public class GetServiceStateFullListService : IGetService<ResultGetServiceStateFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetServiceStateFullListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetServiceStateFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var srvState = _context.ServiceStates.AsNoTracking().ToList();
                var srvReqType = _context.ServiceRequestTypes.AsNoTracking().ToList();
                var srvStateList = srvState.Join(srvReqType, state => state.ServiceRequestTypeId_FK, req => req.Id,
                    (state, req) => new
                    {
                        finalStateId = state.Id,
                        finalStateName = state.StateName,
                        finalRequestType = req.Name
                    }).AsQueryable();

                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    srvStateList = srvStateList.Where(p => p.finalStateName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalSrvStateList = srvStateList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                                                        .Select(p => new RequestGetServiceStateFullListDto
                                                        {
                                                            Id = p.finalStateId,
                                                            StateName = p.finalStateName,
                                                            ServiceRequestType = p.finalRequestType,
                                                        }).ToList();
                if (finalSrvStateList.Any())
                {
                    return new ResultDto<ResultGetServiceStateFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetServiceStateFullListDto
                        {
                            srvStateList = finalSrvStateList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetServiceStateFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetServiceStateFullListDto
                    {
                        srvStateList = null,
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
                    return new ResultDto<ResultGetServiceStateFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceStateFullListDto>
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
                    return new ResultDto<ResultGetServiceStateFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceStateFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
