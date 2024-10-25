using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.FacadPatterns;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.IServices.InternalServices;
using Aliasys.Application.Services.InternalServices.UserServices.Queries.FindUser;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Aliasys.Domain.Entities.UserEntities;
using Aliasys.Infrastructure.ExternalServices.NotificationServices;
using Aliasys.Infrastructure.ExternalServices.NotificationServices.EmailService;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.CreateServiceRequest
{
    public class CreateServiceRequestService : ICreateService<long, RequestCreateServiceRequestDto>
    {
        private readonly IDataBaseContext _context;
        private readonly IServiceCategoryFacad _serviceCategoryFacad;
        private readonly IServiceRequestTypeFacad _serviceRequestTypeFacad;
        private readonly INotificationService<Task, RequestEmailServiceDto> _notificationService;
        private readonly IUserInGroupFacad _userInGroupFacad;
        private readonly IUserFacad _userFacad;

        public CreateServiceRequestService(IDataBaseContext context,
            IServiceCategoryFacad serviceCategoryFacad,
            IServiceRequestTypeFacad serviceRequestTypeFacad,
            INotificationService<Task, RequestEmailServiceDto> notificationService,
            IUserInGroupFacad userInGroupFacad,
            IUserFacad userFacad)
        {
            _context = context;
            _serviceCategoryFacad = serviceCategoryFacad;
            _serviceRequestTypeFacad = serviceRequestTypeFacad;
            _notificationService = notificationService;
            _userInGroupFacad = userInGroupFacad;
            _userFacad = userFacad;
        }

        public ResultDto<long> Create(RequestDto<RequestCreateServiceRequestDto> request)
        {
            var find = _serviceCategoryFacad.FindServiceCategoryWithIdService.Find(new RequestDto<int>
            {
                Parameter = request.Parameter.ServiceCategoryId
            }).Data.UserGroupId_FK;

            var useringroup = _userInGroupFacad.UserInGroupService.GetUserInGroupWithGroupId(find);
            var userListForSendEmail = new List<ResultFindDto>();
            foreach (var item in useringroup)
            {
                var user = _userFacad.FindUserWithIdService.Find(new RequestDto<int> { Parameter = item.UserId_FK }).Data;
                if (user != null)
                {
                    userListForSendEmail.Add(user);
                }
            }

            try
            {
                string cat = _serviceCategoryFacad.FindServiceCategoryWithIdService.Find(new RequestDto<int>
                {
                    Parameter = request.Parameter.ServiceCategoryId
                }).Data.Name.Split(" ")[0];

                string reqType = _serviceRequestTypeFacad.FindServiceRequestTypeWithIdService.Find(new RequestDto<int>
                {
                    Parameter = request.Parameter.ServiceRequestTypeId
                }).Data.BriefName.Split(" ")[0];

                long reqCount = _context.ServiceRequests.Where(p => p.RequestNumber.Contains(cat)).Count();
                string registerDate = DateTime.Now.ToString("yyyy-MM-dd").ToString().Replace("-", "");

                if (cat != null && reqType != null)
                {
                    ServiceRequest newServiceRequest = new ServiceRequest()
                    {
                        RequestNumber = $"{cat}-{reqType.ToUpper()}-{registerDate}-{(reqCount + 1).ToString("000000")}",
                        OwnerUserId_FK = request.Parameter.OwnerUserId,
                        ServiceCategoryId_FK = request.Parameter.ServiceCategoryId,
                        ServiceRequestTypeId_FK = request.Parameter.ServiceRequestTypeId,
                        ServicePriority = request.Parameter.ServicePriority,
                        OccurDateTime = request.Parameter.OccurDateTime,
                        ServiceAffected = request.Parameter.ServiceAffected == "1" ? true : false,
                        ImpactOn = request.Parameter.ImpactOn,
                        Title = request.Parameter.Title,
                    };
                    _context.ServiceRequests.Add(newServiceRequest);
                    _context.SaveChanges();

                    foreach (var user in userListForSendEmail)
                    {
                        _notificationService.Send(new RequestDto<RequestEmailServiceDto>
                        {
                            Parameter = new RequestEmailServiceDto
                            {
                                Body = "A request with the title " + newServiceRequest.Title + " and code " + newServiceRequest.RequestNumber +
                             "has been sent to you. Please review it at your earliest convenience.",
                                Subject = newServiceRequest.Title + " with code " + newServiceRequest.RequestNumber + " has been created.",
                                UserEmail = user.Email
                            }
                        });
                    }

                    var supplicantEmail = _userFacad.FindUserWithIdService.Find(new RequestDto<int>
                    {
                        Parameter = newServiceRequest.OwnerUserId_FK
                    });

                    _notificationService.Send(new RequestDto<RequestEmailServiceDto>
                    {
                        Parameter = new RequestEmailServiceDto
                        {
                            Body = "Dears,\r\nThe TT No. " + newServiceRequest.RequestNumber + " has been successfully created.\r\nBR,\r\nAliaSaaS BC Support",
                            Subject = newServiceRequest.Title + " with code " + newServiceRequest.RequestNumber + " has been created.",
                            UserEmail = supplicantEmail.Data.Email
                        }
                    });

                    return new ResultDto<long>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Created,
                        Message = Messages.ShowMessages(MessageTitleType.Request_Create).Message,
                        Data = newServiceRequest.Id
                    };

                }
                return new ResultDto<long>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Fail).Message
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<long>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<long>
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
                    return new ResultDto<long>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<long>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
