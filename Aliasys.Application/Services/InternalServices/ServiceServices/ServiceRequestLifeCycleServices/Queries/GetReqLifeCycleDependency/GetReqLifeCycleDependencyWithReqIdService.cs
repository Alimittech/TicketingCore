using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetReqLifeCycleDependency
{
    public class GetReqLifeCycleDependencyWithReqIdService : IGetService<ResultGetReqLifeCycleDependencyDto, long>//long is TicketId
    {
        private readonly IDataBaseContext _context;
        public GetReqLifeCycleDependencyWithReqIdService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetReqLifeCycleDependencyDto> Get(RequestDto<long> request)
        {
            try
            {
                var req = _context.ServiceRequests.AsNoTracking().Where(p => p.Id == request.Parameter).ToList();
                //----- User -----
                var user = _context.Users.AsNoTracking().ToList();
                var userDep = _context.UserInOrgOpunitPoses.AsNoTracking().Where(p => p.UserId_FK == req[0].OwnerUserId_FK).ToList();
                var opunit = _context.OperationUnits.AsNoTracking().Where(p => p.Id == userDep[0].OperationUnitId_FK).ToList();
                var pos = _context.Positions.AsNoTracking().Where(p => p.Id == userDep[0].PositionId_FK).ToList();
                var resultUser = user.Join(userDep, usr => usr.Id, usd => usd.UserId_FK,
                                          (usr, usd) => (usr, usd))
                                     .Join(opunit, usr => usr.usd.OperationUnitId_FK, opu => opu.Id,
                                          (usr, opu) => (usr, opu))
                                     .Join(pos, usr => usr.usr.usd.PositionId_FK, pos => pos.Id,
                                          (usr, pos) => new UserInfoDto
                                          {
                                              Id = usr.usr.usr.Id,
                                              DisplayName = usr.usr.usr.DisplayName,
                                              Email = usr.usr.usr.Email,
                                              PhoneNo = usr.usr.usr.PhoneNumber,
                                              ExtNo = usr.usr.usr.ExtentionNumber,
                                              OperationUnit = usr.opu.Name,
                                              Position = pos.Name
                                          }).FirstOrDefault();

                //----- Request -----
                var cat = _context.ServiceCategories.AsNoTracking().Where(p => p.Id == req[0].ServiceCategoryId_FK).ToList();
                var reqtype = _context.ServiceRequestTypes.AsNoTracking().Where(p => p.Id == req[0].ServiceRequestTypeId_FK).ToList();
                var resultReq = req.Join(cat, rq => rq.ServiceCategoryId_FK, ct => ct.Id,
                                        (rq, ct) => (rq, ct))
                                    .Join(reqtype, rq => rq.rq.ServiceRequestTypeId_FK, rt => rt.Id,
                                        (rq, rt) => (rq, rt))
                                    .Join(user, rq => rq.rq.rq.OwnerUserId_FK, us => us.Id,
                                        (rq, us) => new ServiceRequestDto
                                        {
                                            ReqId = rq.rq.rq.Id,
                                            RequestNumber = rq.rq.rq.RequestNumber,
                                            OwnerUser = us.DisplayName,
                                            ServiceCategory = rq.rq.ct.Name,
                                            ServiceRequestType = rq.rt.Name,
                                            ServicePriority = rq.rq.rq.ServicePriority.ToString(),
                                            OccuerDateTime = rq.rq.rq.OccurDateTime,
                                            ServiceAffected = rq.rq.rq.ServiceAffected,
                                            ImpactOn = rq.rq.rq.ImpactOn.ToString(),
                                            Title = rq.rq.rq.Title
                                        }).FirstOrDefault();

                //----- Life Cycle -----
                var reqLifeCycles = _context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.ServiceRequestId_FK == req[0].Id).ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var states = _context.ServiceStates.AsNoTracking().ToList();
                var phases = _context.ServicePhases.AsNoTracking().ToList();
                
                var resultLifeCycle = reqLifeCycles.Join(req, rlc => rlc.ServiceRequestId_FK, req => req.Id,
                                                        (rlc, req) => (rlc, req))
                                                   .Join(states, rlc => rlc.rlc.ServiceStateId_FK, stt => stt.Id,
                                                        (rlc, stt) => (rlc, stt))
                                                   .Join(phases, rlc => rlc.rlc.rlc.ServicePhaseId_FK, phs => phs.Id,
                                                        (rlc, phs) => (rlc, phs))
                                                   .Join(user, rlc => rlc.rlc.rlc.rlc.ProcessUserId, usr => usr.Id,
                                                        (rlc, usr) => (rlc, usr))
                                                   .Join(users, rlc => rlc.rlc.rlc.rlc.rlc.AssignedUserId, usa => usa.Id,
                                                        (rlc, usa) => (rlc, usa))
                                                   .Join(users, rlc => rlc.rlc.rlc.rlc.rlc.rlc.ProcessUserId, usp => usp.Id,
                                                        (rlc, usp) => new ServiceRequestLifeCycleDto
                                                        {
                                                            ReqLifeCycleId = rlc.rlc.rlc.rlc.rlc.rlc.Id,
                                                            StateName = rlc.rlc.rlc.rlc.stt.StateName,
                                                            PhaseName = rlc.rlc.rlc.phs.PhaseName,
                                                            ProcessAction = (ProcessAction)Enum.Parse(typeof(ProcessAction), rlc.rlc.rlc.rlc.rlc.rlc.ProcessAction.ToString()),
                                                            RootCause = rlc.rlc.rlc.rlc.rlc.rlc.RootCauseId,
                                                            SubCause = rlc.rlc.rlc.rlc.rlc.rlc.SubCauseId,
                                                            Description = rlc.rlc.rlc.rlc.rlc.rlc.Description,
                                                            AttachmentFileName = rlc.rlc.rlc.rlc.rlc.rlc.AttachmentFileName,
                                                            ProcessUser = usp.DisplayName,
                                                            AssignedUser = rlc.usa.DisplayName,
                                                            CreatedDateTime = rlc.rlc.rlc.rlc.rlc.rlc.CreatedDateTime
                                                        }).ToList();

                if (resultUser != null && resultReq != null && resultLifeCycle != null)
                {
                    return new ResultDto<ResultGetReqLifeCycleDependencyDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = new ResultGetReqLifeCycleDependencyDto
                        {
                            UserInfo = resultUser,
                            ServiceRequest = resultReq,
                            ServiceRequestLifeCycles = resultLifeCycle
                        }
                    };
                }
                return new ResultDto<ResultGetReqLifeCycleDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetReqLifeCycleDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetReqLifeCycleDependencyDto>
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
                    return new ResultDto<ResultGetReqLifeCycleDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetReqLifeCycleDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
