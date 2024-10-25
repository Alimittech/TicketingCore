using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Queries.FindOperationUnit
{
    public class FindOperationUnitWithNameService : IFindService<ResultFindOperationUnitDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindOperationUnitWithNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindOperationUnitDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.OperationUnits.AsNoTracking().FirstOrDefault(p => p.Equals(request.Parameter));
                if (result != null)
                {
                    return new ResultDto<ResultFindOperationUnitDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindOperationUnitDto
                        {
                            Id = result.Id,
                            Code = result.Code,
                            Name = result.Name,
                        }
                    };
                }
                return new ResultDto<ResultFindOperationUnitDto>
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
                    return new ResultDto<ResultFindOperationUnitDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOperationUnitDto>
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
                    return new ResultDto<ResultFindOperationUnitDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindOperationUnitDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
