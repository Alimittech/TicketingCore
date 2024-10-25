using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetOpunitManager
{
    public class GetOpunitManagerService : IGetService<string, RequestUserDependencyDto>
    {
        private readonly IDataBaseContext _context;
        public GetOpunitManagerService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<string> Get(RequestDto<RequestUserDependencyDto> request)
        {
            try
            {
                var pos = _context.Positions.AsNoTracking().FirstOrDefault(p => p.Name == "Manager");
                var userDep = _context.UserInOrgOpunitPoses.AsNoTracking().FirstOrDefault(p => p.OperationUnitId_FK == request.Parameter.OperationUnitId
                                                                                    && p.PositionId_FK == pos.Id);
                var UserManagerName = _context.Users.AsNoTracking().FirstOrDefault(p => p.Id == userDep.UserId_FK);
                if (UserManagerName != null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item Found!",
                        Data = UserManagerName.DisplayName
                    };
                }
                return new ResultDto<string>
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
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
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
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
