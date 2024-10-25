using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.PositionServices.Commands.UpdatePosition
{
    public class UpdatePositionService : IUpdateService<int?, RequestUpdatePositionDto>
    {
        private readonly IDataBaseContext _context;
        public UpdatePositionService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<int?> Update(RequestDto<RequestUpdatePositionDto> request)
        {
            try
            {
                var findPosition = _context.Positions.FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findPosition == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }
                var findPositionName = _context.Positions.FirstOrDefault(p => p.Name == request.Parameter.Name && request.Parameter.Name != findPosition.Name);
                if (findPositionName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }
                findPosition.Name = request.Parameter.Name;
                findPosition.UpdatedDateTime = DateTime.Now;
                _context.Positions.Update(findPosition);
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
