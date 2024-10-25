using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.GetUserDependency
{
    public class GetUserDepWithUserIdService : IGetService<ResultGetUserDepDto, int>
    {
        private readonly IDataBaseContext _context;
        public GetUserDepWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetUserDepDto> Get(RequestDto<int> request)
        {
            try
            {
                var userDeps = _context.UserInOrgOpunitPoses.AsNoTracking().Where(p => p.UserId_FK == request.Parameter).ToList();
                var users = _context.Users.AsNoTracking().ToList();
                var orgs = _context.Organizations.AsNoTracking().ToList();
                var opunits = _context.OperationUnits.AsNoTracking().ToList();
                var positions = _context.Positions.AsNoTracking().ToList();
                var result = userDeps.Join(users, dep => dep.UserId_FK, usr => usr.Id,
                                            (dep, usr) => (dep, usr))
                                     .Join(orgs, dep => dep.dep.OrganizationId_FK, org => org.Id,
                                            (dep, org) => (dep, org))
                                     .Join(opunits, dep => dep.dep.dep.OperationUnitId_FK, opu => opu.Id,
                                            (dep, opu) => (dep, opu))
                                     .Join(positions, dep => dep.dep.dep.dep.PositionId_FK, pos => pos.Id,
                                            (dep, pos) => new
                                            {
                                                finalId = dep.dep.dep.dep.Id,
                                                finalUserId = dep.dep.dep.dep.UserId_FK,
                                                finalUserName = dep.dep.dep.usr.UserName,
                                                finalOrgId = dep.dep.org.Id,
                                                finalOrganization = dep.dep.org.Name,
                                                finalOpunitId = dep.opu.Id,
                                                finalOperationUnit = dep.opu.Name,
                                                finalPosId = pos.Id,
                                                finalPosition = pos.Name
                                            }).LastOrDefault();
                if (result != null)
                {
                    return new ResultDto<ResultGetUserDepDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item(s) Found",
                        Data = new ResultGetUserDepDto
                        {
                            Id = result.finalId,
                            UserId = result.finalUserId,
                            UserName = result.finalUserName,
                            OrganizationId = result.finalOrgId,
                            Organization = result.finalOrganization,
                            OperationUnitId = result.finalOpunitId,
                            OperationUnit = result.finalOperationUnit,
                            PositionId = result.finalPosId,
                            Position = result.finalPosition,
                        }
                    };
                }
                return new ResultDto<ResultGetUserDepDto>
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
                    return new ResultDto<ResultGetUserDepDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserDepDto>
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
                    return new ResultDto<ResultGetUserDepDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetUserDepDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
