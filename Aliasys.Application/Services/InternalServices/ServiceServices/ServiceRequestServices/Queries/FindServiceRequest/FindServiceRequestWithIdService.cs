using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestServices.Queries.FindServiceRequest
{
    public class FindServiceRequestWithIdService : IFindService<ResultFindServiceRequestDto, long>
    {
        private readonly IDataBaseContext _context;
        public FindServiceRequestWithIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultFindServiceRequestDto> Find(RequestDto<long> request)
        {
            try
            {
                var result = _context.ServiceRequests.AsNoTracking().SingleOrDefault(p => p.Id == request.Parameter);
                if (result != null)
                {
                    return new ResultDto<ResultFindServiceRequestDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item Found!",
                        Data = new ResultFindServiceRequestDto
                        {
                            Id = result.Id,
                            RequestNumber = result.RequestNumber,
                            OwnerUserId = result.OwnerUserId_FK,
                            ServiceCategoryId = result.ServiceCategoryId_FK,
                            ServiceRequestTypeId = result.ServiceRequestTypeId_FK,
                            ServicePriority = result.ServicePriority,
                            OccurDateTime = result.OccurDateTime,
                            ServiceAffected = result.ServiceAffected,
                            ImpactOn = result.ImpactOn,
                            Title = result.Title,
                        }
                    };
                }
                return new ResultDto<ResultFindServiceRequestDto>
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
                    return new ResultDto<ResultFindServiceRequestDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceRequestDto>
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
                    return new ResultDto<ResultFindServiceRequestDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindServiceRequestDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
