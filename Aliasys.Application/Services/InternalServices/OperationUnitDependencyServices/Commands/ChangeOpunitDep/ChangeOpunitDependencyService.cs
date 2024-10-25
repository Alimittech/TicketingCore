using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Aliasys.Domain.Entities.OperationUnitEntities;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OperationUnitDependencyServices.Commands.ChangeOpunitDep
{
    public class ChangeOpunitDependencyService : IChangeService<RequestChangeOpunitDependencyDto>
    {
        private readonly IDataBaseContext _context;
        public ChangeOpunitDependencyService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Change(RequestDto<RequestChangeOpunitDependencyDto> request)
        {
            try
            {
                var result = _context.OperationUnitDependencies.AsNoTracking().FirstOrDefault(p => p.OperationUnitId_FK == request.Parameter.OperationUnitId);
                if (result != null)
                {
                    if (result.OperationUnitId_FK == request.Parameter.OperationUnitId &&
                        result.OrganizationId_FK == request.Parameter.OrganizationId &&
                        result.ParentOperationUnitId_FK == request.Parameter.ParentOperationUnitId &&
                        result.ManagerId_FK == request.Parameter.ManagerId)
                    {
                        return new ResultDto
                        {
                            IsSuccess = true,
                            ActionType = ActionType.NotChange,
                            Message = Messages.ShowMessages(MessageTitleType.Request_NotChanged).Message
                        };
                    }
                    //check dependency for this record...
                    //remove old record
                    result.IsDeleted = true;
                    result.DeletedDateTime = DateTime.Now;
                    _context.OperationUnitDependencies.Update(result);
                    _context.SaveChanges();
                }
                OperationUnitDependency newOpunitDep = new OperationUnitDependency()
                {
                    OrganizationId_FK = request.Parameter.OrganizationId,
                    OperationUnitId_FK = request.Parameter.OperationUnitId,
                    ParentOperationUnitId_FK = request.Parameter.ParentOperationUnitId,
                    ManagerId_FK = request.Parameter.ManagerId
                };
                _context.OperationUnitDependencies.Add(newOpunitDep);
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    ActionType = ActionType.Changed,
                    Message = Messages.ShowMessages(MessageTitleType.Request_Change).Message
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