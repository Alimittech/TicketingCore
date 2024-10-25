using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.SystemComponentEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Commands.CreateSystem
{
    public class CreateSystemService : ICreateService<int?, RequestCreateSystemDto>
    {
        private readonly IDataBaseContext _context;
        public CreateSystemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<int?> Create(RequestDto<RequestCreateSystemDto> request)
        {
            try
            {
                var result = _context.SystemComponents.AsNoTracking().FirstOrDefault(p => p.Name.Trim().ToLower() == request.Parameter.Name.Trim().ToLower());
                if (result != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "The entered name is already exist!"
                    };
                }
                result = null;
                SystemComponent newSystem = new SystemComponent
                {
                    Name = request.Parameter.Name.Trim(),
                    Description = request.Parameter.Description,
                };
                var res = _context.SystemComponents.AsNoTracking().ToList();
                if (_context.SystemComponents.AsNoTracking().Any())
                {
                    newSystem.ParentSystemId = request.Parameter.ParentSystem;
                }
                else
                {
                    newSystem.ParentSystemId = 1;
                }
                _context.SystemComponents.Add(newSystem);
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
