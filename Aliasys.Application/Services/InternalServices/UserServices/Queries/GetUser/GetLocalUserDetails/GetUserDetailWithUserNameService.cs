using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.GetUser.GetLocalUserDetails
{
    public class GetUserDetailWithUserNameService : IGetService<ResultGetUserDetailWithUserNameDto, string>
    {
        private readonly IDataBaseContext _context;
        public GetUserDetailWithUserNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetUserDetailWithUserNameDto> Get(RequestDto<string> request)
        {
            try
            {
                var user = _context.Users.AsNoTracking().FirstOrDefault(p => p.UserName == request.Parameter);
                var userDep = _context.UserInOrgOpunitPoses.AsNoTracking().FirstOrDefault(p => p.UserId_FK == user.Id);
                var org = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.Id == userDep.OrganizationId_FK);
                var opunit = _context.OperationUnits.AsNoTracking().FirstOrDefault(p => p.Id == userDep.OperationUnitId_FK);
                var pos = _context.Positions.AsNoTracking().FirstOrDefault(p => p.Id == userDep.PositionId_FK);

                int managerId = _context.OperationUnitDependencies.AsNoTracking().Where(p => p.OperationUnitId_FK == userDep.OperationUnitId_FK).OrderBy(c => c.Id).LastOrDefault().ManagerId_FK;
                var managerName = _context.Users.AsNoTracking().FirstOrDefault(p => p.Id == managerId).DisplayName;
                var userInRoll = _context.UserInRolls.AsNoTracking().FirstOrDefault(p => p.UserId_FK == user.Id);
                var roll = _context.UserRolls.AsNoTracking().FirstOrDefault(p => p.Id == userInRoll.RollId_FK);
                var result = new ResultGetUserDetailWithUserNameDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ExtentionNumber = user.ExtentionNumber,
                    PersonCode = user.PersonCode,
                    IsActive = user.IsActive,
                    Organization = org.Name,
                    OperationUnit = opunit.Name,
                    Position = pos.Name,
                    ManagerId = managerId,
                    ManagerName = managerName,
                    UserRollName = roll.RollName,
                    ImageName = user.ImageName
                };
                if (result != null)
                {
                    return new ResultDto<ResultGetUserDetailWithUserNameDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item Found",
                        Data = result
                    };
                }
                return new ResultDto<ResultGetUserDetailWithUserNameDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "Item No Found!",
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetUserDetailWithUserNameDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserDetailWithUserNameDto>
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
                    return new ResultDto<ResultGetUserDetailWithUserNameDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserDetailWithUserNameDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
