using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServicePhaseService.Queries.GetServicePhase
{
    public class GetServicePhaseAllService : IGetService<List<ResultGetServicePhaseAllDto>>
    {
        private readonly IDataBaseContext _context;
        public GetServicePhaseAllService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetServicePhaseAllDto>> Get()
        {
            try
            {
                var getAllPhase = _context.ServicePhases.AsNoTracking().Select(p => new ResultGetServicePhaseAllDto
                {
                    Id = p.Id,
                    ServiceRequestType = p.ServiceRequestTypeId_FK,
                    PhaseName = p.PhaseName,
                }).ToList();
                if (getAllPhase != null)
                {
                    return new ResultDto<List<ResultGetServicePhaseAllDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item(s) Found!",
                        Data = getAllPhase
                    };
                }
                return new ResultDto<List<ResultGetServicePhaseAllDto>>
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
                    return new ResultDto<List<ResultGetServicePhaseAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetServicePhaseAllDto>>
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
                    return new ResultDto<List<ResultGetServicePhaseAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetServicePhaseAllDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
