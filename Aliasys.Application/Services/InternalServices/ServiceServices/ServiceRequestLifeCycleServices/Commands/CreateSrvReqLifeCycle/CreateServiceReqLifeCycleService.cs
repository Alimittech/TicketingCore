using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.UserEntities;
using Aliasys.Infrastructure.ExternalServices.NotificationServices;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestLifeCycleServices.Commands.CreateSrvReqLifeCycle
{
    public class CreateServiceReqLifeCycleService : ICreateService<int?, RequestCreateSrvReqLifeCycleDto>
    {
        private readonly IDataBaseContext _context;
        private readonly INotificationService<Task, RequestEmailServiceDto> _notificationService;
        private readonly IServiceRequestFacad _serviceRequestFacad;
        private readonly IUserFacad _userFacad;
        public CreateServiceReqLifeCycleService(IDataBaseContext context, IUserFacad userFacad, INotificationService<Task, RequestEmailServiceDto> notificationService, IServiceRequestFacad serviceRequestFacad)
        {
            _context = context;
            _userFacad = userFacad;
            _notificationService = notificationService;
            _serviceRequestFacad = serviceRequestFacad;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateSrvReqLifeCycleDto> request)
        {
            try
            {
                ServiceRequestLifeCycle newReqLifeCycle = new ServiceRequestLifeCycle
                {
                    ServiceRequestId_FK = request.Parameter.ServiceRequestId,
                    ServiceStateId_FK = request.Parameter.ServiceStateId,
                    ServicePhaseId_FK = request.Parameter.ServicePhaseId,
                    ProcessAction = request.Parameter.ProcessAction,
                    RootCauseId = request.Parameter.RootCauseId,
                    SubCauseId = request.Parameter.SubCauseId,
                    Description = request.Parameter.Description,
                    AttachmentFileName = request.Parameter.AttachmentFileName,
                    ProcessUserId = request.Parameter.ProcessUserId,
                    AssignedUserId = request.Parameter.AssignedUserId,
                    AssignedGroupId = request.Parameter.AssignedGroupId,
                };
                _context.ServiceRequestLifeCycles.Add(newReqLifeCycle);
                _context.SaveChanges();
                var userResult = _serviceRequestFacad.FindServiceRequestWithIdService.Find(new RequestDto<long>
                {
                    Parameter = request.Parameter.ServiceRequestId
                });
                var emailUserResult = _userFacad.FindUserWithIdService.Find(new RequestDto<int>
                {
                    Parameter = userResult.Data.OwnerUserId
                });
                var userProvider = _userFacad.FindUserWithIdService.Find(new RequestDto<int>
                {
                    Parameter = newReqLifeCycle.ProcessUserId
                });

                var assignto = _userFacad.FindUserWithIdService.Find(new RequestDto<int>
                {
                    Parameter = newReqLifeCycle.AssignedUserId
                });
                if(assignto.Data.UserName != userProvider.Data.UserName)
                {
                    var bodymail =
                                        (newReqLifeCycle.ProcessAction != ProcessAction.Assign) ?
                                             GenerateMessage(userResult.Data.RequestNumber, userResult.Data.Title, newReqLifeCycle.ProcessAction.Value, userProvider.Data.UserName) :
                                             GenerateMessage(userResult.Data.RequestNumber, userResult.Data.Title, newReqLifeCycle.ProcessAction.Value, assignto.Data.UserName);

                    _notificationService.Send(new RequestDto<RequestEmailServiceDto>
                    {
                        Parameter = new RequestEmailServiceDto
                        {
                            Body = bodymail,
                            Subject = "Your Request Change State;",
                            UserEmail = emailUserResult.Data.Email
                        }
                    });

                    if (newReqLifeCycle.ProcessAction == ProcessAction.Assign)
                    {
                        _notificationService.Send(new RequestDto<RequestEmailServiceDto>
                        {
                            Parameter = new RequestEmailServiceDto
                            {
                                Body = "A request with the title " + userResult.Data.Title + " and code " + userResult.Data.RequestNumber +
                                 "has been sent to you. Please review it at your earliest convenience.",
                                Subject = "A request Assign to you from " + userProvider.Data.UserName + ";",
                                UserEmail = assignto.Data.Email
                            }
                        });
                    }

                }

                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Created,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Create).Message
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
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
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    
        public string GenerateMessage (string requestId, string title ,ProcessAction process, string username)
        {
            if (process != ProcessAction.Assign)
            {
                return "Dears,\r\nThe TT No. " + requestId + " for " + title + " is " + process.ToString() + " by " + username + ".\r\n\r\nBR,\r\nAliaSaaS BC Support";
            }
            else
            {
                return "Dears,\r\nThe TT No. " + requestId + " for " + title + " is " + process.ToString() + " to " + username + ".\r\n\r\nBR,\r\nAliaSaaS BC Support";
            }

        }
    }
}
