using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithUserId
{
    public class GetAllServiceReqWithUserIdService : IGetService<ResultGetAllServiceReqDto, RequestAllGetSrvReqWithUserIdDto>
    {
        private readonly IDataBaseContext _context;
        public GetAllServiceReqWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllServiceReqDto> Get(RequestDto<RequestAllGetSrvReqWithUserIdDto> request)
        {
            try
            {
                var Reqs = _context.ServiceRequests.AsNoTracking().Where(x => x.OwnerUserId_FK == request.Parameter.UserId).ToList();
                var Cats = _context.ServiceCategories.AsNoTracking().ToList();
                var ReqTypes = _context.ServiceRequestTypes.AsNoTracking().ToList();
                var ReqLifeCycles = _context.ServiceRequestLifeCycles.AsNoTracking().GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList();
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
                                            (req, phs) => (req, phs))
                                        .Join(users, req => req.req.req.req.req.req.OwnerUserId_FK, usr => usr.Id,
                                            (req, usr) => (req, usr))
                                        .Join(users, req => req.req.req.req.rlc.ProcessUserId, usr1 => usr1.Id,
                                            (req, usr1) => (req, usr1))
                                        .Join(users, req => req.req.req.req.req.rlc.AssignedUserId, usr2 => usr2.Id,
                                            (req, usr2) => new
                                            {
                                                finalRequestId = req.req.req.req.req.req.req.req.Id,
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
                if (!string.IsNullOrWhiteSpace(request.Parameter.Pagination.SearchKey))
                {
                    result = result.Where(p => p.finalCategory.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalReqNumber.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalReqType.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalStateName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                            || p.finalPhaseName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower()));
                }
                var finalResult = result.ToPaged(request.Parameter.Pagination.Page, request.Parameter.Pagination.PageSize, out rowCount)
                                                            .Select(p => new ResGetAllServiceReqDto
                                                            {
                                                                Id = p.finalRequestId,
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
                    return new ResultDto<ResultGetAllServiceReqDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetAllServiceReqDto
                        {
                            srvRequestList = finalResult,
                            RowsCount = rowCount,
                            Page = request.Parameter.Pagination.Page,
                            PageSize = request.Parameter.Pagination.PageSize
                        }
                    };
                }
                return new ResultDto<ResultGetAllServiceReqDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "No record found",
                    Data = new ResultGetAllServiceReqDto
                    {
                        srvRequestList = null,
                        RowsCount = rowCount,
                        Page = request.Parameter.Pagination.Page,
                        PageSize = request.Parameter.Pagination.PageSize
                    }
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetAllServiceReqDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllServiceReqDto>
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
                    return new ResultDto<ResultGetAllServiceReqDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllServiceReqDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
