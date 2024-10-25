using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.GetServiceRequest.GetServiceReqDetails
{
    public class GetServiceRequestDetailsService : IGetService<ResultGetServiceRequestDetailsDto, long>
    {
        private readonly IDataBaseContext _context;
        public GetServiceRequestDetailsService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetServiceRequestDetailsDto> Get(RequestDto<long> request)
        {
            try
            {
                var result = _context.ServiceRequests.AsNoTracking().SingleOrDefault(p => p.Id == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultGetServiceRequestDetailsDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Item Found!",
                        Data = new ResultGetServiceRequestDetailsDto
                        {
                            Id = result.Id,
                            RequestNumber = result.RequestNumber,
                            OwnerUserId_FK = result.OwnerUserId_FK,
                            ServiceCategoryId_FK = result.ServiceCategoryId_FK,
                            ServiceRequestTypeId_FK = result.ServiceRequestTypeId_FK,
                            ServicePriority = result.ServicePriority,
                            OccurDateTime = result.OccurDateTime,
                            ServiceAffected = result.ServiceAffected,
                            ImpactOn = result.ImpactOn,
                            Title = result.Title,
                        }
                    };
                }
                return new ResultDto<ResultGetServiceRequestDetailsDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "Item Not Found!",
                };

            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetServiceRequestDetailsDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceRequestDetailsDto>
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
                    return new ResultDto<ResultGetServiceRequestDetailsDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetServiceRequestDetailsDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
