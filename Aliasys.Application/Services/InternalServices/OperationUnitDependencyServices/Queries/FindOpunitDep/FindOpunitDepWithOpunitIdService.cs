using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Queries.FindOpunitDep
{
    public class FindOpunitDepWithOpunitIdService : IFindService<ResultFindOpunitDepDto, int>
    {
        private readonly IDataBaseContext _context;
        public FindOpunitDepWithOpunitIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindOpunitDepDto> Find(RequestDto<int> request)
        {
            try
            {
                var result = _context.OperationUnitDependencies.AsNoTracking().FirstOrDefault(p => p.OperationUnitId_FK == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultFindOpunitDepDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Record found!",
                        Data = new ResultFindOpunitDepDto
                        {
                            Id = result.Id,
                            OrganizationId = result.OrganizationId_FK,
                            OperationUnitId = result.OperationUnitId_FK,
                            ParentOpunitId = result.ParentOperationUnitId_FK,
                            ManagerId = result.ManagerId_FK,
                        }
                    };
                }
                return new ResultDto<ResultFindOpunitDepDto>
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
