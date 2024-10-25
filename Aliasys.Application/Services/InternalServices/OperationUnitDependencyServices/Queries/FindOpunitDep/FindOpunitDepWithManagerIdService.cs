using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Queries.FindOpunitDep
{
    public class FindOpunitDepWithManagerIdService : IFindService<ResultFindOpunitDepDto>
    {
        private readonly IDataBaseContext _context;
        public FindOpunitDepWithManagerIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindOpunitDepDto> Find(int Id)
        {
            try
            {
                var findManagerId = _context.OperationUnitDependencies.AsNoTracking().FirstOrDefault(p => p.ManagerId_FK == Id);
                //var userDep = _context.UserInOrgOpunitPoses.AsNoTracking().FirstOrDefault(p => p.UserId_FK == findManagerId.Id);
                //var org = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.Id == userDep.OrganizationId_FK);
                //var opunit = _context.OperationUnits.AsNoTracking().FirstOrDefault(p => p.Id == findManagerId.OperationUnitId_FK);
                //var parentOpunit = _context.OperationUnits.AsNoTracking().FirstOrDefault(p => p.Id == findManagerId.ParentOperationUnitId_FK);
                //var user = _context.Users.AsNoTracking().FirstOrDefault(p => p.Id == Id);

                //if (findManagerId != null)
                //{
                //    return new ResultDto<ResultFindOpunitDepDto>
                //    {
                //        IsSuccess = true,
                //        ActionType = ActionType.IsExist,
                //        Message = "Item found!",
                //        Data = new ResultFindOpunitDepDto
                //        {
                //            Id = findManagerId.Id,
                //            OrganizationId = org.Id,
                //            Organization = org.Name,
                //            OperationUnitId = opunit.Id,
                //            OperationUnit = opunit.Name,
                //            ParentOpunitId = parentOpunit.Id,
                //            ParentOpunit = parentOpunit.Name,
                //            ManagerId = Id,
                //            Manager = user.DisplayName
                //        }
                //    };
                //}
                if (findManagerId != null)
                {
                    return new ResultDto<ResultFindOpunitDepDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item found!"
                    };
                }
                return new ResultDto<ResultFindOpunitDepDto>
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
                    return new ResultDto<ResultFindOpunitDepDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOpunitDepDto>
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
                    return new ResultDto<ResultFindOpunitDepDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOpunitDepDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
