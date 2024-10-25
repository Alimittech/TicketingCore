using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Commands.CreateUser
{
    public class CreateUserService : ICreateService<int?, RequestCreateUserDto>
    {
        private readonly IDataBaseContext _context;
        public CreateUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateUserDto> request)
        {
            try
            {
                var result = _context.Users.AsNoTracking().FirstOrDefault(p => p.UserName.Trim().ToLower() == request.Parameter.UserName.Trim().ToLower()
                                                                            || p.Email.Trim().ToLower() == request.Parameter.Email.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "The entered name/email is already exist!"
                    };
                }
                result = null;
                User newUser = new User
                {
                    UserName = request.Parameter.UserName.Trim(),
                    DisplayName = request.Parameter.DisplayName,
                    Email = request.Parameter.Email.Trim(),
                    PhoneNumber = request.Parameter.PhoneNumber.Trim(),
                    ExtentionNumber = request.Parameter.ExtentionNumber.Trim(),
                    IsActive = request.Parameter.IsActive,
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return new ResultDto<int?>
                {
                    IsSuccess = true,
                    ActionType = ActionType.Created,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Create).Message
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
