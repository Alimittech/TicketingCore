using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.ServiceServices.ServiceRequestTypeServices.Commands.UpdateServiceRequestType
{
    public class UpdateServiceRequestTypeService : IUpdateService<int?, RequestUpdateServiceRequestTypeDto>
    {
        private readonly IDataBaseContext _context;
        public UpdateServiceRequestTypeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<int?> Update(RequestDto<RequestUpdateServiceRequestTypeDto> request)
        {
            try
            {
                var findReqType = _context.ServiceRequestTypes.AsNoTracking().FirstOrDefault(p => p.Id == request.Parameter.Id);
                if (findReqType == null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Null,
                        Message = "No Record Found!"
                    };
                }

                var findSrvReqTypeName = _context.ServiceRequestTypes.AsNoTracking()
                    .FirstOrDefault(p => p.Name == request.Parameter.Name
                                      && request.Parameter.Name != findReqType.Name);
                if (findSrvReqTypeName != null)
                {
                    findSrvReqTypeName = null;
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This name is already registered!"
                    };
                }

                var findSrvReqTypeBriefName = _context.ServiceRequestTypes.AsNoTracking()
                    .FirstOrDefault(p => p.BriefName == request.Parameter.BriefName
                                      && request.Parameter.BriefName != findReqType.BriefName);
                if (findSrvReqTypeBriefName != null)
                {
                    return new ResultDto<int?>
                    {
                        IsSuccess = false,
                        ActionType = ActionType.IsExist,
                        Message = "This briefname is already registered!"
                    };
                }
                findReqType.RequestType = request.Parameter.RequestType;
                findReqType.Name = request.Parameter.Name;
                findReqType.BriefName = request.Parameter.BriefName;
                findReqType.UpdatedDateTime = DateTime.Now;
                _context.ServiceRequestTypes.Update(findReqType);
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
