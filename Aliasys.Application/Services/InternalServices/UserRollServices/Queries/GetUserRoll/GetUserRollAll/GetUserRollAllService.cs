using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.Queries.GetUserRoll.GetUserRollAll
{
    public class GetUserRollAllService : IGetService<List<ResultGetUserRollAllDto>>
    {
        private readonly IDataBaseContext _context;
        public GetUserRollAllService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetUserRollAllDto>> Get()
        {
            try
            {
                var result = _context.UserRolls.AsNoTracking().Select(p => new ResultGetUserRollAllDto
                {
                    Id = p.Id,
                    Name = p.RollName,
                    Description = p.Description
                }).ToList();
                if (result != null)
                {
                    return new ResultDto<List<ResultGetUserRollAllDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = result
                    };
                }
                return new ResultDto<List<ResultGetUserRollAllDto>>
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
                    return new ResultDto<List<ResultGetUserRollAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetUserRollAllDto>>
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
                    return new ResultDto<List<ResultGetUserRollAllDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetUserRollAllDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
