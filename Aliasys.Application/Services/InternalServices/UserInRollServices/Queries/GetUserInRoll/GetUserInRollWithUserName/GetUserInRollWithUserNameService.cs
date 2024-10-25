using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Dtos;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserInRollServices.Queries.GetUserInRoll.GetUserInRollWithUserName
{
    public class GetUserInRollWithUserNameService : IGetService<List<ResultGetUserInRollWithUserNameDto>, string>
    {
        private readonly IDataBaseContext _context;
        public GetUserInRollWithUserNameService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetUserInRollWithUserNameDto>> Get(RequestDto<string> request)
        {
            try
            {
                var user = _context.Users.AsNoTracking().FirstOrDefault(x => x.UserName == request.Parameter);
                var rolls = _context.UserRolls.AsNoTracking().ToList();
                var userInRolls = _context.UserInRolls.AsNoTracking().Where(x => x.UserId_FK == user.Id).ToList();
                var result = userInRolls.Join(rolls, uir => uir.RollId_FK, rol => rol.Id,
                                            (uir, rol) => new ResultGetUserInRollWithUserNameDto
                                            {
                                                UserId = user.Id,
                                                RollId = uir.RollId_FK,
                                                RollName = rol.RollName
                                            }).ToList();
                if (result != null)
                {
                    return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Completed,
                        Data = result
                    };
                }
                return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Null,
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
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
                    return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto<List<ResultGetUserInRollWithUserNameDto>>
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
