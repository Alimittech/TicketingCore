using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Commands.ChangeStateUser
{
    public class ChangeStateUserService : IChangeStateUserService
    {
        private readonly IDataBaseContext _context;
        public ChangeStateUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto ChangeStateUser(int UserId)
        {
            try
            {
                var user = _context.Users.Find(UserId);
                if (user != null)
                {
                    user.IsActive = !user.IsActive;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Message = Messages.ShowMessages(MessageTitleType.Request_Update).Message
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    ActionType = ActionType.Failed,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Fail).Message
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto
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
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
