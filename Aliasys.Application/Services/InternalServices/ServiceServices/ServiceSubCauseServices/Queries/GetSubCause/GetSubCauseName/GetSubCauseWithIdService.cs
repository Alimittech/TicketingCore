using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceSubCauseServices.Queries.GetSubCause.GetSubCauseName
{
    public class GetSubCauseWithIdService : IGetService<string, int>
    {
        private readonly IDataBaseContext _context;
        public GetSubCauseWithIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<string> Get(RequestDto<int> request)
        {
            try
            {
                var result = _context.ServiceSubCauses.Find(request.Parameter);
                if (result != null)
                {
                    return new ResultDto<string>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item Found!",
                        Data = result.SubCauseName
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
