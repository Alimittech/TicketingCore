using Aliasys.Application.Interfaces.Contexts;
using Aliasys.Application.Interfaces.Operations;
using Aliasys.Common.Constants;
using Aliasys.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Aliasys.Application.Services.InternalServices.OrganizationServices.Commands.DeleteOrganization
{
    public class DeleteOrganizationService : IDeleteService
    {
        private readonly IDataBaseContext _context;
        public DeleteOrganizationService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Delete(int Id)
        {
            try
            {
                var checkChildThis = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.ParentId_FK == Id && p.Id != 1);
                if (checkChildThis != null)
                {
                    checkChildThis = null;
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Failed,
                        Message = "Action Failed, this department is parent of another department!"
                    };
                }
                var orgList = _context.Organizations.AsNoTracking().FirstOrDefault(p => p.Id == Id);
                if (orgList.Id == 1)
                {
                    orgList = null;
                    return new ResultDto
                    {
                        IsSuccess = false,
                        ActionType = ActionType.Failed,
                        Message = "The first record can't be deleted!"
                    };
                }
                else if (orgList != null)
                {
                    //Check OperionUnits & Position Data Recode
                    //CustomExtentionMethods.GetAllEntitiesDependingOn<DbSet<Organization>>(_context,DbSet<Organization> entity)


                    orgList.IsDeleted = true;
                    orgList.DeletedDateTime = DateTime.Now;
                    _context.Organizations.Update(orgList);
                    _context.SaveChanges();
                    orgList = null;
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
