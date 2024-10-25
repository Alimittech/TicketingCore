using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.Queries.FindUserRoll
{
    public class FindUserRollWithNameService : IFindService<ResultFindUserRollDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindUserRollWithNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindUserRollDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.UserRolls.AsNoTracking().FirstOrDefault(p => p.Equals(request.Parameter));
                if (result != null)
                {
                    return new ResultDto<ResultFindUserRollDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindUserRollDto
                        {
                            Id = result.Id,
                            Name = result.RollName,
                            Description = result.Description
                        }
                    };
                }
                return new ResultDto<ResultFindUserRollDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "No Record Found!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultFindUserRollDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindUserRollDto>
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
                    return new ResultDto<ResultFindUserRollDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindUserRollDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
