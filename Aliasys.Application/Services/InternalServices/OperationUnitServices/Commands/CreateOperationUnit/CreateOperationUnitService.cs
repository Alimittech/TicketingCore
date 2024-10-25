using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.OperationUnitEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitServices.Commands.CreateOperationUnit
{
    public class CreateOperationUnitService : ICreateService<int?, RequestCreateOperationUnitDto>
    {
        private readonly IDataBaseContext _context;
        public CreateOperationUnitService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<int?> Create(RequestDto<RequestCreateOperationUnitDto> request)
        {
            try
            {
                var result = _context.OperationUnits.AsNoTracking().Where(p => p.Name.Trim().ToLower() == request.Parameter.Name.Trim().ToLower()
                                                                            || p.Code == request.Parameter.Code).ToList();
                if (result.Count != 0)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "the entered name/code is already exist!"
                    };
                    result = null;
                }
                OperationUnit newOperationUnit = new OperationUnit
                {
                    Name = request.Parameter.Name,
                    Code = request.Parameter.Code,
                };
                _context.OperationUnits.Add(newOperationUnit);
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
