using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Queries.GetAllReqLifeCycleWithReqId
{
    public class GetAllReqLifeCycleWithReqIdService : IGetService<ResultGetAllReqLifeCycleDto, int>
    {
        private readonly IDataBaseContext _context;
        public GetAllReqLifeCycleWithReqIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllReqLifeCycleDto> Get(RequestDto<int> request)
        {
            try
            {
                var reqLifeCycles = _context.ServiceRequestLifeCycles.AsNoTracking().Where(p => p.Id == request.Parameter).ToList();
                var states = _context.ServiceStates.AsNoTracking().ToList();
                var phases = _context.ServicePhases.AsNoTracking().ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var rootCauses = _context.ServiceRootCauses.AsNoTracking().ToList();
                var subCauses = _context.ServiceSubCauses.AsNoTracking().ToList();

                var result = reqLifeCycles.Join(states, rlc => rlc.ServiceStateId_FK, stt => stt.Id,
                                                (rlc, stt) => (rlc, stt))
                                          .Join(phases, rlc => rlc.rlc.ServicePhaseId_FK, phs => phs.Id,
                                                (rlc, phs) => (rlc, phs))
                                          .Join(users, rlc => rlc.rlc.rlc.AssignedUserId, usr => usr.Id,
                                                (rlc, usr) => (rlc, usr))
                                          .Join(users, rlc => rlc.rlc.rlc.rlc.ProcessUserId, user => user.Id,
                                                (rlc, user) => (rlc,user))
                                          .Join(rootCauses, rlc => rlc.rlc.rlc.rlc.rlc.RootCauseId, rtc => rtc.Id,
                                                (rlc, rtc) => (rlc, rtc))
                                          .Join(subCauses, rlc => rlc.rlc.rlc.rlc.rlc.rlc.SubCauseId, sbc => sbc.Id,
                                                (rlc, sbc) => new RequestGetAllReqLifeCycleDto
                                                {
                                                    Id = rlc.rlc.rlc.rlc.rlc.rlc.Id,
                                                    ServiceRequestId = rlc.rlc.rlc.rlc.rlc.rlc.ServiceRequestId_FK,
                                                    ServiceStateName = rlc.rlc.rlc.rlc.rlc.stt.StateName,
                                                    ServicePhaseName = rlc.rlc.rlc.rlc.phs.PhaseName,
                                                    ProcessAction = (ProcessAction)Enum.Parse(typeof(ProcessAction), rlc.rlc.rlc.rlc.rlc.rlc.ProcessAction.ToString()),
                                                    RootCause = rlc.rtc.RootCauseName,
                                                    SubCause = sbc.SubCauseName,
                                                    Description = rlc.rlc.rlc.rlc.rlc.rlc.Description,
                                                    ProcessUserDisplayName = rlc.rlc.rlc.usr.DisplayName,
                                                    AssignedUserDisplayName = rlc.rlc.user.DisplayName
                                                }).ToList();



                if (result != null)
                {
                    return new ResultDto<ResultGetAllReqLifeCycleDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item Found",
                        Data = new ResultGetAllReqLifeCycleDto
                        {
                            reqLifeCycleList = result
                        }
                    };
                }
                return new ResultDto<ResultGetAllReqLifeCycleDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "Item Not Found!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetAllReqLifeCycleDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllReqLifeCycleDto>
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
                    return new ResultDto<ResultGetAllReqLifeCycleDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetAllReqLifeCycleDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
