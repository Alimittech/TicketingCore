using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.GetServicePhase
{
    public class GetServicePhaseFullListService : IGetService<ResultGetServicePhaseFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetServicePhaseFullListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetServicePhaseFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var srvPhase = _context.ServicePhases.AsNoTracking().ToList();
                var srvReqType = _context.ServiceRequestTypes.AsNoTracking().ToList();
                var srvPhaseList = srvPhase.Join(srvReqType, phase => phase.ServiceRequestTypeId_FK, req => req.Id,
                    (phase, req) => new
                    {
                        finalPhaseId = phase.Id,
                        finalPhaseName = phase.PhaseName,
                        finalRequestType = req.Name
                    }).AsQueryable();

                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    srvPhaseList = srvPhaseList.Where(p => p.finalPhaseName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalSrvPhaseList = srvPhaseList.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                                                        .Select(p => new RequestGetServicePhaseFullListDto
                                                        {
                                                            Id = p.finalPhaseId,
                                                            PhaseName = p.finalPhaseName,
                                                            ServiceRequestType = p.finalRequestType,
                                                        }).ToList();
                if (finalSrvPhaseList.Any())
                {
                    return new ResultDto<ResultGetServicePhaseFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetServicePhaseFullListDto
                        {
                            srvPhaseList = finalSrvPhaseList,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetServicePhaseFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetServicePhaseFullListDto
                    {
                        srvPhaseList = null,
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
                    return new ResultDto<ResultGetServicePhaseFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServicePhaseFullListDto>
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
                    return new ResultDto<ResultGetServicePhaseFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServicePhaseFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
