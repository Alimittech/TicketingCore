using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState
{
    public class FindServiceStateWithNameService : IFindService<ResultFindServiceStateDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindServiceStateWithNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindServiceStateDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.ServiceStates.AsNoTracking().SingleOrDefault(p => p.StateName.Trim().ToLower() == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultFindServiceStateDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item Found!",
                        Data = new ResultFindServiceStateDto
                        {
                            Id = result.Id,
                            ServiceRequestTypeId_FK = result.ServiceRequestTypeId_FK,
                            StateName = result.StateName,
                        }
                    };
                }
                return new ResultDto<ResultFindServiceStateDto>
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
                    return new ResultDto<ResultFindServiceStateDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceStateDto>
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
                    return new ResultDto<ResultFindServiceStateDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceStateDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
