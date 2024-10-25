using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Common.Paginations;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetAllServiceReqWithManagerId
{
    public class GetAllServiceReqWithManagerIdService : IGetService<ResultGetAllServiceReqDto, RequestGetAllServiceReqInUnitDto>
    {
        private readonly IDataBaseContext _context;
        public GetAllServiceReqWithManagerIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllServiceReqDto> Get(RequestDto<RequestGetAllServiceReqInUnitDto> request)
        {
            try
            {
                var checkUserIsManager = _context.OperationUnitDependencies.AsNoTracking().FirstOrDefault(p => p.ManagerId_FK == request.Parameter.ManagerId);
                IQueryable<ResGetAllServiceReqDto> result;
                if (checkUserIsManager != null)
                {
                    var cats = _context.ServiceCategories.AsNoTracking().ToList();
                    var reqTypes = _context.ServiceRequestTypes.AsNoTracking().ToList();
                    var users = _context.Users.AsNoTracking().ToList();
                    var reqs = _context.ServiceRequests.AsNoTracking().ToList();
                    var reqLifeCycles = _context.ServiceRequestLifeCycles.AsNoTracking().GroupBy(p => p.ServiceRequestId_FK).Select(p => p.OrderBy(c => c.Id).LastOrDefault()).ToList();
                    var states = _context.ServiceStates.AsNoTracking().ToList();
                    var phases = _context.ServicePhases.AsNoTracking().ToList();
                    if (checkUserIsManager.ParentOperationUnitId_FK == 1)
                    {
                        result = reqs.Join(cats, req => req.ServiceCategoryId_FK, cat => cat.Id,
                                            (req, cat) => (req, cat))
                                     .Join(reqTypes, req => req.req.ServiceRequestTypeId_FK, srt => srt.Id,
                                            (req, srt) => (req, srt))
                                     .Join(reqLifeCycles, req => req.req.req.Id, rlc => rlc.ServiceRequestId_FK,
                                            (req, rlc) => (req, rlc))
                                     .Join(states, req => req.rlc.ServiceStateId_FK, stt => stt.Id,
                                            (req, stt) => (req, stt))
                                     .Join(phases, req => req.req.rlc.ServicePhaseId_FK, phs => phs.Id,
                                            (req, phs) => (req, phs))
                                     .Join(users, req => req.req.req.req.req.req.OwnerUserId_FK, usr => usr.Id,
                                            (req, usr) => (req, usr))
                                     .Join(users, req => req.req.req.req.rlc.ProcessUserId, usr1 => usr1.Id,
                                            (req, usr1) => (req, usr1))
                                     .Join(users, req => req.req.req.req.req.rlc.AssignedUserId, usr2 => usr2.Id,
                                             (req, usr2) => new ResGetAllServiceReqDto
                                             {
                                                 Id = req.req.req.req.req.req.req.req.Id,
                                                 UserId = req.req.req.req.req.req.req.req.OwnerUserId_FK,
                                                 RequestNumber = req.req.req.req.req.req.req.req.RequestNumber,
                                                 ServiceCategory = req.req.req.req.req.req.req.cat.Name,
                                                 ServiceRequestType = req.req.req.req.req.req.srt.Name,
                                                 RegisterDateTime = req.req.req.req.req.req.req.req.CreatedDateTime,
                                                 StateName = req.req.req.req.stt.StateName,
                                                 PhaseName = req.req.req.phs.PhaseName,
                                                 OwnerName = req.req.usr.DisplayName,
                                                 ProcessorName = req.usr1.DisplayName,
                                                 AssignedName = usr2.DisplayName
                                             }).AsQueryable();
                    }
                    else
                    {
                        var opunitUsers = _context.UserInOrgOpunitPoses.AsNoTracking().Where(p => p.OperationUnitId_FK == checkUserIsManager.OperationUnitId_FK).ToList();
                        result = reqs.Join(opunitUsers, req => req.OwnerUserId_FK, opu => opu.UserId_FK,
                                                (req, opu) => (req, opu))
                                         .Join(cats, req => req.req.ServiceCategoryId_FK, cat => cat.Id,
                                                (req, cat) => (req, cat))
                                         .Join(reqTypes, req => req.req.req.ServiceRequestTypeId_FK, srt => srt.Id,
                                                (req, srt) => (req, srt))
                                         .Join(reqLifeCycles, req => req.req.req.req.Id, rlc => rlc.ServiceRequestId_FK,
                                                (req, rlc) => (req, rlc))
                                         .Join(states, req => req.rlc.ServiceStateId_FK, stt => stt.Id,
                                                (req, stt) => (req, stt))
                                         .Join(phases, req => req.req.rlc.ServicePhaseId_FK, phs => phs.Id,
                                                (req, phs) => (req, phs))
                                         .Join(users, req => req.req.req.req.req.req.req.OwnerUserId_FK, usr => usr.Id,
                                                (req, usr) => (req, usr))
                                         .Join(users, req => req.req.req.req.rlc.ProcessUserId, usr1 => usr1.Id,
                                                (req, usr1) => (req, usr1))
                                         .Join(users, req => req.req.req.req.req.rlc.AssignedUserId, usr2 => usr2.Id,
                                                 (req, usr2) => new ResGetAllServiceReqDto
                                                 {
                                                     Id = req.req.req.req.req.req.req.req.req.Id,
                                                     UserId = req.req.req.req.req.req.req.req.req.OwnerUserId_FK,
                                                     RequestNumber = req.req.req.req.req.req.req.req.req.RequestNumber,
                                                     ServiceCategory = req.req.req.req.req.req.req.cat.Name,
                                                     ServiceRequestType = req.req.req.req.req.req.srt.Name,
                                                     RegisterDateTime = req.req.req.req.req.req.req.req.req.CreatedDateTime,
                                                     StateName = req.req.req.req.stt.StateName,
                                                     PhaseName = req.req.req.phs.PhaseName,
                                                     OwnerName = req.req.usr.DisplayName,
                                                     ProcessorName = req.usr1.DisplayName,
                                                     AssignedName = usr2.DisplayName
                                                 }).AsQueryable();
                    }
                    
                    int rowCount = 0;
                    if (!string.IsNullOrWhiteSpace(request.Parameter.Pagination.SearchKey))
                    {
                        result = result.Where(p => p.ServiceCategory.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                                || p.RequestNumber.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                                || p.ServiceRequestType.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                                || p.StateName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower())
                                                || p.PhaseName.Contains(request.Parameter.Pagination.SearchKey.Trim().ToLower()));
                    }
                    var finalResult = result.ToPaged(request.Parameter.Pagination.Page, request.Parameter.Pagination.PageSize, out rowCount)
                                                            .Select(p => new ResGetAllServiceReqDto
                                                            {
                                                                Id = p.Id,
                                                                UserId = p.UserId,
                                                                RequestNumber = p.RequestNumber,
                                                                ServiceCategory = p.ServiceCategory,
                                                                ServiceRequestType = p.ServiceRequestType,
                                                                RegisterDateTime = p.RegisterDateTime,
                                                                StateName = p.StateName,
                                                                PhaseName = p.PhaseName,
                                                                OwnerName = p.OwnerName,
                                                                ProcessorName = p.ProcessorName,
                                                                AssignedName = p.AssignedName,
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
                return new ResultDto<ResultGetAllServiceReqDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "No record found!"
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
