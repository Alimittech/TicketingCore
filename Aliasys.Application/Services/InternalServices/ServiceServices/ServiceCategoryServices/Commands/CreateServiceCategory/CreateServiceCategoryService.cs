using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryService : ICreateService<int?, RequestCreateServiceCategoryDto>
    {
        private readonly IDataBaseContext _context;
        public CreateServiceCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Create(RequestDto<RequestCreateServiceCategoryDto> request)
        {
            try
            {
                var result = _context.ServiceCategories.AsNoTracking().FirstOrDefault(p => p.Name.Trim().ToLower() == request.Parameter.Name.Trim().ToLower());
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
                ServiceCategory newIncCategory = new ServiceCategory
                {
                    Name = request.Parameter.Name,
                    UserGroupId_FK = request.Parameter.UserGroupId_FK
                };
                _context.ServiceCategories.Add(newIncCategory);
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
