using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserServices.Queries.FindUser
{
    public class FindUserWithEmailService : IFindService<ResultFindDto, string>
    {
        private readonly IDataBaseContext _context;
        public FindUserWithEmailService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultFindDto> Find(RequestDto<string> request)
        {
            try
            {
                var result = _context.Users.AsNoTracking().FirstOrDefault(p => p.Email.Trim().ToLower() == request.Parameter.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<ResultFindDto>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.IsExist,
                        Message = "Item(s) Found!",
                        Data = new ResultFindDto
                        {
                            Id = result.Id,
                            UserName = result.UserName.Trim(),
                            DisplayName = result.DisplayName.Trim(),
                            Email = result.Email.Trim(),
                            PhoneNumber = result.PhoneNumber.Trim(),
                            ExtentionNumber = result.ExtentionNumber.Trim(),
                            IsActive = result.IsActive,
                        }
                    };
                }
                return new ResultDto<ResultFindDto>
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
                    return new ResultDto<ResultFindDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindDto>
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
                    return new ResultDto<ResultFindDto>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<ResultFindDto>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
