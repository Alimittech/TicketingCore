using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Queries.GetSystem.GetSystemAll
{
    public class GetSystemAllService : IGetService<List<ResultGetSystemAllDto>>
    {
        private readonly IDataBaseContext _context;
        public GetSystemAllService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetSystemAllDto>> Get()
        {
            try
            {
                var result = _context.SystemComponents.AsNoTracking().Select(p => new ResultGetSystemAllDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ParentSystemId = p.ParentSystemId
                }).ToList();
                if (result != null)
                {
                    return new ResultDto<List<ResultGetSystemAllDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = result
                    };
                }
                return new ResultDto<List<ResultGetSystemAllDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Data = null
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<ResultGetSystemAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetSystemAllDto>>
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
                    return new ResultDto<List<ResultGetSystemAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetSystemAllDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
