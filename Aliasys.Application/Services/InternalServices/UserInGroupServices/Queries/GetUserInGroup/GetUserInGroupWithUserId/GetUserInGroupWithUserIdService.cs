using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserInGroupServices.Queries.GetUserInGroup.GetUserInGroupWithUserId
{
    public class GetUserInGroupWithUserIdService : IGetService<List<int>, int>
    {
        private readonly IDataBaseContext _context;
        public GetUserInGroupWithUserIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<int>> Get(RequestDto<int> request)
        {
            try
            {
                var userGroupList = _context.UserInGroups.AsNoTracking().Where(x => x.UserId_FK == request.Parameter).ToList();
                
                if (userGroupList != null)
                {
                    return new ResultDto<List<int>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = "Items Found!",
                        Data = userGroupList.Select(x => x.UserGroupId_FK).ToList()
                    };
                }
                return new ResultDto<List<int>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                    Message = "Item Not Found!",
                    Data = null
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<int>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<int>>
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
                    return new ResultDto<List<int>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<int>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
