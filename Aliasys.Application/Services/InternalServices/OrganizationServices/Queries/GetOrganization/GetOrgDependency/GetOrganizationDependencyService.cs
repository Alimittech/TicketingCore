using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Queries.GetOrganization.GetOrgDependency
{
    public class GetOrganizationDependencyService : IGetService<ResultGetOrganizationDependencyDto, int>
    {
        private readonly IDataBaseContext _context;
        public GetOrganizationDependencyService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetOrganizationDependencyDto> Get(RequestDto<int> request)
        {
            try
            {
                var org = _context.Organizations.AsNoTracking().FirstOrDefault(x => x.Id == request.Parameter);
                if (org != null)
                {
                    return new ResultDto<ResultGetOrganizationDependencyDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Data = new ResultGetOrganizationDependencyDto
                        {
                            ParentOrganizationId = org.ParentId_FK,
                            RegionId = org.RegionId_FK,
                        }
                    };
                }
                return new ResultDto<ResultGetOrganizationDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetOrganizationDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "سایت در دسترس نمی باشد" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOrganizationDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "سایت در دسترس نمی باشد" + ex.Message,
                };
            }
            catch (Exception ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<ResultGetOrganizationDependencyDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "خطایی رخ داده است" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultGetOrganizationDependencyDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "خطایی رخ داده است",
                };
            }
        }
    }
}
