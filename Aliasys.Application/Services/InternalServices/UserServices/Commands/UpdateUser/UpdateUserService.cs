using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Commands.UpdateUser
{
    public class UpdateUserService : IUpdateService<int?, RequestUpdateUserDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateUserDto> request)
        {
            try
            {
                var findUser = _context.Users.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findUser == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }

                var findUserName = _context.Users.FirstOrDefault(p => p.UserName.Trim().ToLower() == request.Parameter.UserName.Trim().ToLower()
                                                                   && request.Parameter.UserName.Trim().ToLower() != findUser.UserName.Trim().ToLower());
                if (findUserName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }

                var findUserEmail = _context.Users.FirstOrDefault(p => p.Email.Trim().ToLower() == request.Parameter.Email.Trim().ToLower()
                                                                    && request.Parameter.Email.Trim().ToLower() != findUser.Email.Trim().ToLower());
                if (findUserEmail != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This email is already registered!"
                    };
                }

                findUser.UserName = request.Parameter.UserName.Trim();
                findUser.DisplayName = request.Parameter.DisplayName.Trim();
                findUser.Email = request.Parameter.Email.Trim();
                findUser.PhoneNumber = request.Parameter.PhoneNumber.Trim();
                findUser.ExtentionNumber = request.Parameter.ExtentionNumber.Trim();
                findUser.UpdatedDateTime = DateTime.Now;
                findUser.IsActive = request.Parameter.IsActive;
                _context.Users.Update(findUser);
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
