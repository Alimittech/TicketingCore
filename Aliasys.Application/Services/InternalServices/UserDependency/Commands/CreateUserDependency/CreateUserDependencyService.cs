using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserDependency.Commands.CreateUserDependency
{
    public class CreateUserDependencyService : ICreateService<int?, RequestCreateUserDependencyDto>
    {
        private readonly IDataBaseContext _context;
        public CreateUserDependencyService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateUserDependencyDto> request)
        {
            try
            {
                var result = _context.UserInOrgOpunitPoses.AsNoTracking().FirstOrDefault(p => p.UserId_FK == request.Parameter.UserId_FK
                                                                                           && p.OrganizationId_FK == request.Parameter.OrganizationId_FK
                                                                                           && p.OperationUnitId_FK == request.Parameter.OperationUnitId_FK
                                                                                           && p.PositionId_FK == request.Parameter.PositionId_FK);
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "A user with the entered details has already been registered"
                    };
                }
                result = null;
                UserInOrgOpunitPos newUserDependency = new UserInOrgOpunitPos
                {
                    UserId_FK = request.Parameter.UserId_FK,
                    OrganizationId_FK = request.Parameter.OrganizationId_FK,
                    OperationUnitId_FK = request.Parameter.OperationUnitId_FK,
                    PositionId_FK = request.Parameter.PositionId_FK,
                };
                _context.UserInOrgOpunitPoses.Add(newUserDependency);
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
