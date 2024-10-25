using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.CreateServiceRequestType
{
    public class CreateServiceRequestTypeService : ICreateService<int?, RequestCreateServiceRequestTypeDto>
    {
        private readonly IDataBaseContext _context;
        public CreateServiceRequestTypeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateServiceRequestTypeDto> request)
        {
            try
            {
                var result = _context.ServiceRequestTypes.AsNoTracking().FirstOrDefault(p => p.Name.Trim().ToLower() == request.Parameter.Name.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "the entered name is already exist!"
                    };
                }
                result = null;
                ServiceRequestType newServiceIncRequestType = new ServiceRequestType
                {
                    RequestType = request.Parameter.RequestType,
                    Name = request.Parameter.Name,
                    BriefName = request.Parameter.BriefName,
                };
                _context.ServiceRequestTypes.Add(newServiceIncRequestType);
                _context.SaveChanges();
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
    }
}
