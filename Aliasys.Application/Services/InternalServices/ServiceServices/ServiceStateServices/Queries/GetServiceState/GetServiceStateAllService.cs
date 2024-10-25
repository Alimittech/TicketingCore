using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState
{
    public class GetServiceStateAllService : IGetService<List<ResultGetServiceStateAllDto>>
    {
        private readonly IDataBaseContext _context;
        public GetServiceStateAllService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetServiceStateAllDto>> Get()
        {
            try
            {
                var getAllState = _context.ServiceStates.AsNoTracking().Select(p => new ResultGetServiceStateAllDto
                {
                    Id = p.Id,
                    ServiceRequestTypeId = p.ServiceRequestTypeId_FK,
                    StateName = p.StateName,
                }).ToList();
                if (getAllState != null)
                {
                    return new ResultDto<List<ResultGetServiceStateAllDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item(s) Found!",
                        Data = getAllState
                    };
                }
                return new ResultDto<List<ResultGetServiceStateAllDto>>
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
                    return new ResultDto<List<ResultGetServiceStateAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetServiceStateAllDto>>
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
                    return new ResultDto<List<ResultGetServiceStateAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetServiceStateAllDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
