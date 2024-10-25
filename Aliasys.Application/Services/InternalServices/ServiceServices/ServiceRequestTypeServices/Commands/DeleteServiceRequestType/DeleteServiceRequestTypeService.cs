using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.DeleteServiceRequestType
{
    public class DeleteServiceRequestTypeService : IDeleteService
    {
        private readonly IDataBaseContext _context;
        public DeleteServiceRequestTypeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Delete(int Id)
        {
            try
            {
                var srvReqType = _context.ServiceRequestTypes.AsNoTracking().FirstOrDefault(x => x.Id == Id);
                if (srvReqType.Id == 1)
                {
                    srvReqType = null;
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Failed,
                        Message = "The first record can't be deleted!"
                    };
                }
                else if (srvReqType != null)
                {
                    //check dependency
                    srvReqType.IsDeleted = true;
                    srvReqType.DeletedDateTime = DateTime.Now;
                    _context.ServiceRequestTypes.Update(srvReqType);
                    _context.SaveChanges();
                    srvReqType = null;
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
