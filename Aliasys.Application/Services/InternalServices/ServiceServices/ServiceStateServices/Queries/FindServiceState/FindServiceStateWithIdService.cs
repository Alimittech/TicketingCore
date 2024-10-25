using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.FindServiceState
{
    public class FindServiceStateWithIdService : IFindService<ResultFindServiceStateDto, int>
    {
        private readonly IDataBaseContext _context;
        public FindServiceStateWithIdService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindServiceStateDto> Find(RequestDto<int> request)
        {
            try
            {
                var result = _context.ServiceStates.AsNoTracking().SingleOrDefault(p => p.Id == request.Parameter);
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
