using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Queries.GetServiceState;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRootCauseServices.Queries.GetRootCause.GetRootCauseName
{
    public class GetRootCauseWithIdService : IGetService<string, int>
    {
        private readonly IDataBaseContext _context;
        public GetRootCauseWithIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<string> Get(RequestDto<int> request)
        {
            try
            {
                var result = _context.ServiceRootCauses.Find(request.Parameter);
                if (result != null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item Found!",
                        Data = result.RootCauseName
                    };
                }
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.NotExist,
                    Message = "Item Not Found!"
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
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
                    return new ResultDto<string>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<string>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
