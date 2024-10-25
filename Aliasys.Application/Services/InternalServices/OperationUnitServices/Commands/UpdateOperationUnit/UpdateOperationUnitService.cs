using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.UpdateOperationUnit
{
    public class UpdateOperationUnitService : IUpdateService<int?, RequestUpdateOperationUnitDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateOperationUnitService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<int?> Update(RequestDto<RequestUpdateOperationUnitDto> request)
        {
            try
            {
                var findOpunit = _context.OperationUnits.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findOpunit == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.NotExist,
                        Message = "No Record Found!"
                    };
                }
                var findOpunitName = _context.OperationUnits.FirstOrDefault(p => p.Name == request.Parameter.Name && request.Parameter.Name != findOpunit.Name);
                if (findOpunitName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }
                var findOpunitCode = _context.OperationUnits.FirstOrDefault(p => p.Code == request.Parameter.Code && request.Parameter.Code != findOpunit.Code);
                if (findOpunitCode != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This code is already registered!"
                    };
                }
                findOpunit.Name = request.Parameter.Name;
                findOpunit.Code = request.Parameter.Code;
                findOpunit.UpdatedDateTime = DateTime.Now;
                _context.OperationUnits.Update(findOpunit);
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
