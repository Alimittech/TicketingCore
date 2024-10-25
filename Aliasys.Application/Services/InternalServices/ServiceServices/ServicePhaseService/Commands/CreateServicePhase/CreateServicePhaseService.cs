using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.CreateServicePhase
{
    public class CreateServicePhaseService : ICreateService<int?, RequestCreateServicePhaseDto>
    {
        private readonly IDataBaseContext _context;
        public CreateServicePhaseService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateServicePhaseDto> request)
        {
            try
            {
                var result = _context.ServicePhases.AsNoTracking().FirstOrDefault(p => p.PhaseName.Trim().ToLower() == request.Parameter.PhaseName.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "the entered name/phasemode is already exist!"
                    };
                }

                result = null;
                ServicePhase newIncPhase = new ServicePhase
                {
                    ServiceRequestTypeId_FK = request.Parameter.ServiceRequestTypeId_FK,
                    PhaseName = request.Parameter.PhaseName,
                };
                _context.ServicePhases.Add(newIncPhase);
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
