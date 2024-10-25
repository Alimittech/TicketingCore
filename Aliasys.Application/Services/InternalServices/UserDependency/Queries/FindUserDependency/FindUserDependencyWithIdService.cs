using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.FindUserDependency
{
    public class FindUserDependencyWithIdService : IFindService<ResultFindUserDependencyDto, int>
    {
        private readonly IDataBaseContext _context;
        public FindUserDependencyWithIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindUserDependencyDto> Find(RequestDto<int> request)
        {
            try
            {
                var result = _context.UserInOrgOpunitPoses.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultFindUserDependencyDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindUserDependencyDto
                        {
                            Id = result.Id,
                            UserId = result.UserId_FK,
                            OrganizationId = result.OrganizationId_FK,
                            OperationUnitId = result.OperationUnitId_FK,
                            PositionId = result.PositionId_FK,
                        }
                    };
                }
                return new ResultDto<ResultFindUserDependencyDto>
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
                    return new ResultDto<ResultFindUserDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindUserDependencyDto>
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
                    return new ResultDto<ResultFindUserDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindUserDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
