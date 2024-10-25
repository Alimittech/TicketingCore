using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceCategoryServices.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryService : IDeleteService
    {
        private readonly IDataBaseContext _context;
        public DeleteServiceCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Delete(int Id)
        {
            try
            {
                var srvCat = _context.ServiceCategories.AsNoTracking().FirstOrDefault(c => c.Id == Id);
                if (srvCat.Id == 1)
                {
                    srvCat = null;
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Failed,
                        Message = "The first record can't be deleted!"
                    };
                }
                else if (srvCat != null)
                {
                    //check dependency
                    srvCat.IsDeleted = true;
                    srvCat.DeletedDateTime = DateTime.Now;
                    _context.ServiceCategories.Update(srvCat);
                    _context.SaveChanges();
                    srvCat = null;
                    return new ResultDto
                    {
                        IsSuccess = true,
                        ActionType = ActionType.Deleted,
                        Message = Messages.ShowMessages(MessageTitleType.Request_Delete).Message
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    ActionType = ActionType.Failed,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Fail).Message
                };
            }
            catch (SqlException ex)
            {
                string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development" || env == "Staging")
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "The application is not accessible!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto
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
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Error,
                        Message = "An error has occurred!" + ex.Message,
                    };
                }
                //env=Production
                return new ResultDto
                {
                    IsSuccess = false,
                    ActionType = ActionType.Error,
                    Message = "An error has occurred!",
                };
            }
        }
    }
}
