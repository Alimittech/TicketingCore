using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.UserRollServices.Commands.UpdateUserRoll
{
    public class UpdateUserRollService : IUpdateService<int?, RequestUpdateUserRollDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateUserRollService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<int?> Update(RequestDto<RequestUpdateUserRollDto> request)
        {
            try
            {
                var findRoll = _context.UserRolls.FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findRoll == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }
                var findRollName = _context.UserRolls.FirstOrDefault(p => p.RollName == request.Parameter.Name && request.Parameter.Name != findRoll.RollName);
                if (findRollName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }
                findRoll.RollName = request.Parameter.Name;
                findRoll.Description = request.Parameter.Description;
                findRoll.UpdatedDateTime = DateTime.Now;
                _context.UserRolls.Update(findRoll);
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
