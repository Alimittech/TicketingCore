using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceStateServices.Commands.DeleteServiceState
{
    public class DeleteServiceStateService : IDeleteService
    {
        private readonly IDataBaseContext _context;
        public DeleteServiceStateService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Delete(int Id)
        {
            try
            {
                var incState = _context.ServiceStates.AsNoTracking().FirstOrDefault(x => x.Id == Id);
                if (incState != null)
                {
                    //check dependency
                    incState.IsDeleted = true;
                    incState.DeletedDateTime = DateTime.Now;
                    _context.ServiceStates.Update(incState);
                    _context.SaveChanges();
                    incState = null;
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
