using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Commands.UpdateServiceRequest
{
    public class UpdateServiceRequestService : IUpdateService<int?, RequestUpdateServiceRequestDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateServiceRequestService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateServiceRequestDto> request)
        {
            try
            {
                var findServiceReq = _context.ServiceRequests.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findServiceReq == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }
                //RequestNumber = Lock, ServiceCategoryId = Lock, OwnerUserId = Lock
                findServiceReq.ServiceRequestTypeId_FK = request.Parameter.ServiceRequestTypeId;
                findServiceReq.ServicePriority = request.Parameter.ServicePriority;
                findServiceReq.OccurDateTime = request.Parameter.OccurDateTime;
                findServiceReq.ServiceAffected = request.Parameter.ServiceAffected;
                findServiceReq.ImpactOn = request.Parameter.ImpactOn;
                findServiceReq.Title = request.Parameter.Title;
                _context.ServiceRequests.Add(findServiceReq);
                _context.SaveChanges();
                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Updated,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Update).Message
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
    }
}
