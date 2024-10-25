using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.SystemServices.Commands.DeleteSystem
{
    public class DeleteSystemService : IDeleteService
    {
        private readonly IDataBaseContext _context;
        public DeleteSystemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Delete(int Id)
        {
            try
            {
                var checkChildThis = _context.SystemComponents.AsNoTracking().FirstOrDefault(p => p.ParentSystemId == Id);
                if (checkChildThis != null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Failed,
                        Message = "Action Failed, this system is parent of another system!"
                    };
                }
                var sysList = _context.SystemComponents.AsNoTracking().FirstOrDefault(p => p.Id == Id);
                if (sysList != null || sysList.Id != 1) 
                {
                    //Check User Permision Data Record
                    sysList.IsDeleted = true;
                    sysList.DeletedDateTime = DateTime.Now;
                    _context.SystemComponents.Update(sysList);
                    _context.SaveChanges();
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
