using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserGroupServices.Commands.UpdateUserGroup
{
    public class UpdateUserGroupService : IUpdateService<int?, RequestUpdateUserGroupDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateUserGroupService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<int?> Update(RequestDto<RequestUpdateUserGroupDto> request)
        {
            try
            {
                var findUserGroup = _context.UserGroups.FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findUserGroup == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }
                var findUserGroupName = _context.UserGroups.FirstOrDefault(p => p.GroupName == request.Parameter.GroupName
                                                                             && request.Parameter.GroupName != findUserGroup.GroupName);
                if (findUserGroupName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered"
                    };
                }
                findUserGroup.GroupName = request.Parameter.GroupName;
                findUserGroup.UpdatedDateTime = DateTime.Now;
                _context.UserGroups.Update(findUserGroup);
                _context.SaveChanges();
                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Updated,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Update).Message
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
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
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<int?>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
