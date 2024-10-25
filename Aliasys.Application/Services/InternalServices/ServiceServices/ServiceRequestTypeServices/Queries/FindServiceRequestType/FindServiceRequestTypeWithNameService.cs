using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Queries.FindServiceRequestType
{
    public class FindServiceRequestTypeWithNameService : IFindService<ResultFindServiceRequestTypeDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindServiceRequestTypeWithNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindServiceRequestTypeDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.ServiceRequestTypes.AsNoTracking().SingleOrDefault(p => p.Name.Trim().ToLower() == request.Parameter.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<ResultFindServiceRequestTypeDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindServiceRequestTypeDto
                        {
                            Id = result.Id,
                            RequestType = result.RequestType,
                            Name = result.Name,
                            BriefName = result.BriefName,
                        }
                    };
                }
                return new ResultDto<ResultFindServiceRequestTypeDto>
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
                    return new ResultDto<ResultFindServiceRequestTypeDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceRequestTypeDto>
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
                    return new ResultDto<ResultFindServiceRequestTypeDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceRequestTypeDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
