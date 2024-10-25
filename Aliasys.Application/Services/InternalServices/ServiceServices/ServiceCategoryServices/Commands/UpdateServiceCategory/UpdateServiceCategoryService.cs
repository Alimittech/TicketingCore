using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryService : IUpdateService<int?, RequestUpdateServiceCategoryDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateServiceCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateServiceCategoryDto> request)
        {
            try
            {
                var findCat = _context.ServiceCategories.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findCat == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }

                var findSrvCateName = _context.ServiceCategories.AsNoTracking().FirstOrDefault(p => p.Name == request.Parameter.Name
                                                                        && request.Parameter.Name != findCat.Name);
                if (findSrvCateName != null)
                {
                    findSrvCateName = null;
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }

                findCat.Name = request.Parameter.Name;
                findCat.UserGroupId_FK = request.Parameter.UserGroupId_FK;
                findCat.UpdatedDateTime = DateTime.Now;
                _context.ServiceCategories.Update(findCat);
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
