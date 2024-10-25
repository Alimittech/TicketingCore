using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.SystemServices.Queries.FindSystem;
using Aliasys.Common.Dtos;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserDependency.Queries.FindUserDependency
{
    public class FindUserDependencyWithUserIdService : IFindService<ResultFindUserDependencyDto, int>
    {
        private readonly IDataBaseContext _context;
        public FindUserDependencyWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindUserDependencyDto> Find(RequestDto<int> request)
        {
            try
            {
                var user = _context.UserInOrgOpunitPoses.AsNoTracking().Where(p => p.UserId_FK == request.Parameter).ToList();
                var org = _context.Organizations.AsNoTracking().ToList();
                var opunit = _context.OperationUnits.AsNoTracking().ToList();
                var pos = _context.Positions.AsNoTracking().ToList();
                var result = user.Join(org, usr => usr.OrganizationId_FK, org => org.Id,
                                        (usr, org) => (usr, org))
                                 .Join(opunit, usr => usr.usr.OperationUnitId_FK, opu => opu.Id,
                                        (usr, opu) => (usr, opu))
                                 .Join(pos, usr => usr.usr.usr.PositionId_FK, pos => pos.Id,
                                        (usr, pos) => new
                                        {
                                            finalId = usr.usr.usr.Id,
                                            finalUserId = usr.usr.usr.UserId_FK,
                                            finalOrgId = usr.usr.usr.OrganizationId_FK,
                                            finalOrgName = usr.usr.org.Name,
                                            finalOpunitId = usr.usr.usr.OperationUnitId_FK,
                                            finalOpunitName = usr.opu.Name,
                                            finalPosId = usr.usr.usr.PositionId_FK,
                                            finalPosName = pos.Name
                                        }).ToList();
                if (result != null)
                {
                    var opunitDep = _context.OperationUnitDependencies.AsNoTracking().FirstOrDefault(p => p.ManagerId_FK == result.First().finalUserId);
                    return new ResultDto<ResultFindUserDependencyDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item found!",
                        Data = new ResultFindUserDependencyDto
                        {
                            Id = result.First().finalId,
                            UserId = result.First().finalUserId,
                            OrganizationId = result.First().finalOrgId,
                            Organization = result.First().finalOrgName,
                            OperationUnitId = result.First().finalOpunitId,
                            OperationUnit = result.First().finalOpunitName,
                            PositionId = result.First().finalPosId,
                            Position = result.First().finalPosName,
                            IsManager = (opunitDep != null) ? true : false,
                        }
                    };
                }
                return new ResultDto<ResultFindUserDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "No record found!"
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
