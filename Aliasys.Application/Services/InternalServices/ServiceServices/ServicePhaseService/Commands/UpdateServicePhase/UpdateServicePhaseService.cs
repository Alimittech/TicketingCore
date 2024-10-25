using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Commands.UpdateServicePhase
{
    public class UpdateServicePhaseService : IUpdateService<int?, RequestUpdateServicePhaseDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateServicePhaseService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateServicePhaseDto> request)
        {
            try
            {
                var findPhase = _context.ServicePhases.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findPhase == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }

                var findSrvPhaseName = _context.ServicePhases.AsNoTracking()
                                                .FirstOrDefault(p => p.PhaseName == request.Parameter.PhaseName
                                                                && request.Parameter.PhaseName != findPhase.PhaseName);
                if (findSrvPhaseName != null)
                {
                    findSrvPhaseName = null;
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }

                findPhase.PhaseName = request.Parameter.PhaseName;
                findPhase.ServiceRequestTypeId_FK = request.Parameter.ServiceRequestTypeId_FK;
                findPhase.UpdatedDateTime = DateTime.Now;
                _context.ServicePhases.Update(findPhase);
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
