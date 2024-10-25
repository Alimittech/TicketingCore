using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqFullList
{
    public class GetServiceRequestFullListService : IGetService<ResultGetServiceRequestFullListDto, PaginationDto>
    {
        private readonly IDataBaseContext _context;
        public GetServiceRequestFullListService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetServiceRequestFullListDto> Get(RequestDto<PaginationDto> request)
        {
            try
            {
                var Reqs = _context.ServiceRequests.AsNoTracking().ToList();
                var Cats = _context.ServiceCategories.AsNoTracking().ToList();
                var ReqTypes = _context.ServiceRequestTypes.AsNoTracking().ToList();
                var ReqLifeCycles = _context.ServiceRequestLifeCycles.AsNoTracking().GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c=> c.Id).LastOrDefault()).ToList();
                var States = _context.ServiceStates.AsNoTracking().ToList();
                var Phases = _context.ServicePhases.AsNoTracking().ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var result = Reqs.Join(Cats, req => req.ServiceCategoryId_FK, cat => cat.Id,
                                            (req, cat) => (req, cat))
                                        .Join(ReqTypes, req => req.req.ServiceRequestTypeId_FK, srt => srt.Id,
                                            (req, srt) => (req, srt))
                                        .Join(ReqLifeCycles, req => req.req.req.Id, rlc => rlc.ServiceRequestId_FK,
                                            (req, rlc) => (req, rlc))
                                        .Join(States, req => req.rlc.ServiceStateId_FK, stt => stt.Id,
                                            (req, stt) => (req, stt))
                                        .Join(Phases, req => req.req.rlc.ServicePhaseId_FK, phs => phs.Id,
                                            (req, phs) => (req,phs))
                                        .Join(users, req => req.req.req.req.req.req.OwnerUserId_FK, usr => usr.Id,
                                            (req,usr)=> (req,usr))
                                        .Join(users, req => req.req.req.req.rlc.ProcessUserId, usr1 => usr1.Id,
                                            (req,usr1) => (req, usr1))
                                        .Join(users, req => req.req.req.req.req.rlc.AssignedUserId, usr2 => usr2.Id,
                                            (req,usr2) => new
                                            {
                                                finalId = req.req.req.req.req.req.req.req.Id,
                                                finalUserId = req.req.req.req.req.req.req.req.OwnerUserId_FK,
                                                finalReqNumber = req.req.req.req.req.req.req.req.RequestNumber,
                                                finalCategory = req.req.req.req.req.req.req.cat.Name,
                                                finalReqType = req.req.req.req.req.req.srt.Name,
                                                finalRegisterDT = req.req.req.req.req.req.req.req.CreatedDateTime,
                                                finalStateName = req.req.req.req.stt.StateName,
                                                finalPhaseName = req.req.req.phs.PhaseName,
                                                finalOwnerName = req.req.usr.DisplayName,
                                                finalProcessorName = req.usr1.DisplayName,
                                                finalAssignedUserName = usr2.DisplayName
                                            }).AsQueryable();


                int rowCount = 0;
                if (!string.IsNullOrWhiteSpace(request.Parameter.SearchKey))
                {
                    result = result.Where(p => p.finalCategory.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                            || p.finalReqNumber.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                            || p.finalReqType.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                            || p.finalStateName.Contains(request.Parameter.SearchKey.Trim().ToLower())
                                            || p.finalPhaseName.Contains(request.Parameter.SearchKey.Trim().ToLower()));
                }
                var finalResult = result.ToPaged(request.Parameter.Page, request.Parameter.PageSize, out rowCount)
                                                            .Select(p => new RequestGetServiceRequestFullListDto
                                                            {
                                                                Id = p.finalId,
                                                                UserId = p.finalUserId,
                                                                RequestNumber = p.finalReqNumber,
                                                                ServiceCategory = p.finalCategory,
                                                                ServiceRequestType = p.finalReqType,
                                                                RegisterDateTime = p.finalRegisterDT,
                                                                StateName = p.finalStateName,
                                                                PhaseName = p.finalPhaseName,
                                                                OwnerName = p.finalOwnerName,
                                                                ProcessorName = p.finalProcessorName,
                                                            }).ToList();
                if (finalResult.Any())
                {
                    return new ResultDto<ResultGetServiceRequestFullListDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetServiceRequestFullListDto
                        {
                            srvRequestList = finalResult,
                            RowsCount = rowCount,
                            Page = request.Parameter.Page,
                            PageSize = request.Parameter.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetServiceRequestFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "There is no item",
                    Data = new ResultGetServiceRequestFullListDto
                    {
                        srvRequestList = null,
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
                    return new ResultDto<ResultGetServiceRequestFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceRequestFullListDto>
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
                    return new ResultDto<ResultGetServiceRequestFullListDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceRequestFullListDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
